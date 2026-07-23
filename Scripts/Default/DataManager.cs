#nullable enable
namespace UniT.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Cysharp.Threading.Tasks;
    using Extensions;
    using Logging;
    using UnityEngine.Scripting;

    public sealed class DataManager : IDataManager, IDisposable
    {
        #region Constructor

        private readonly IReadOnlyList<ISerializer> serializers;
        private readonly IReadOnlyList<IStorage> storages;
        private readonly ILogger logger;

        private readonly Dictionary<string, object> cache = new();
        private readonly Dictionary<Type, IReadOnlyList<(ISerializer, IStorage)>> serializerAndStorageCache = new();

        [Preserve]
        public DataManager(IReadOnlyList<ISerializer> serializers, IReadOnlyList<IStorage> storages, ILoggerManager loggerManager)
        {
            this.serializers = serializers;
            this.storages = storages;
            this.logger = loggerManager.GetLogger(this);
            this.logger.Debug("Constructed");
        }

        #endregion

        UniTask<bool> IDataManager.ContainsAsync(string key, Type type, IProgress<float>? progress, CancellationToken cancellationToken)
        {
            return this.ContainsAsync(key, type, progress, cancellationToken);
        }

        UniTask<bool> IDataManager.ContainsAsync<T>(string key, IProgress<float>? progress, CancellationToken cancellationToken)
        {
            return this.ContainsAsync(key, typeof(T), progress, cancellationToken);
        }

        UniTask<object> IDataManager.LoadAsync(string key, Type type, bool cache, IProgress<float>? progress, CancellationToken cancellationToken)
        {
            return cache
                ? this.cache.GetOrAddAsync(key, static state => state.@this.LoadAsync(state.key, state.type, state.progress, state.cancellationToken), (@this: this, key, type, progress, cancellationToken))
                : this.LoadAsync(key, type, progress, cancellationToken);
        }

        UniTask<T> IDataManager.LoadAsync<T>(string key, bool cache, IProgress<float>? progress, CancellationToken cancellationToken)
        {
            return cache
                ? this.cache.GetOrAddAsync(key, static state => state.@this.LoadAsync<T>(state.key, state.progress, state.cancellationToken).ContinueWith(data => (object)data), (@this: this, key, progress, cancellationToken))
                    .ContinueWith(data => (T)data)
                : this.LoadAsync<T>(key, progress, cancellationToken);
        }

        UniTask IDataManager.SaveAsync(string key, IProgress<float>? progress, CancellationToken cancellationToken)
        {
            if (!this.cache.TryGetValue(key, out var data)) throw new InvalidOperationException($"{key} not loaded");
            return this.SaveAsync(key, data.GetType(), data, progress, cancellationToken);
        }

        UniTask IDataManager.SaveAsync(string key, Type type, object data, bool cache, IProgress<float>? progress, CancellationToken cancellationToken)
        {
            if (cache) this.cache[key] = data;
            return this.SaveAsync(key, type, data, progress, cancellationToken);
        }

        UniTask IDataManager.SaveAsync<T>(string key, T data, bool cache, IProgress<float>? progress, CancellationToken cancellationToken)
        {
            if (cache) this.cache[key] = data;
            return this.SaveAsync(key, data, progress, cancellationToken);
        }

        UniTask IDataManager.SaveAllAsync(IProgress<float>? progress, CancellationToken cancellationToken)
        {
            return this.cache.WhereValue(data => data is IWritableData)
                .ForEachAsync(
                    (kv, progress, cancellationToken) => this.SaveAsync(kv.Key, kv.Value.GetType(), kv.Value, progress, cancellationToken),
                    progress,
                    cancellationToken
                )
                .ContinueWith(this.Flush);
        }

        void IDataManager.Unload(string key) => this.cache.Remove(key);

        void IDataManager.Flush() => this.Flush();

        void IDisposable.Dispose()
        {
            this.cache.Clear();
            this.serializerAndStorageCache.Clear();
            this.logger.Debug("Disposed");
        }

        private async UniTask<bool> ContainsAsync(string key, Type type, IProgress<float>? progress, CancellationToken cancellationToken)
        {
            foreach (var (_, storage) in this.GetSerializerAndStorage(type))
            {
                if (storage is not IReadableStorage readableStorage) continue;
                if (!await readableStorage.ContainsAsync(key, progress, cancellationToken)) continue;
                return true;
            }
            return false;
        }

        private async UniTask<object> LoadAsync(string key, Type type, IProgress<float>? progress, CancellationToken cancellationToken)
        {
            foreach (var (serializer, storage) in this.GetSerializerAndStorage(type))
            {
                if (storage is not IReadableStorage readableStorage) continue;
                if (!await readableStorage.ContainsAsync(key, cancellationToken: cancellationToken)) continue;
                try
                {
                    var rawData = await readableStorage.ReadAsync(key, serializer.RawDataType, progress, cancellationToken);
                    var savedData = await serializer.DeserializeAsync(type, rawData, cancellationToken);
                    this.logger.Debug($"Loaded {key} with '{serializer.GetType().Name}' & '{storage.GetType().Name}'");
                    return savedData;
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException($"Failed to load {key} with '{serializer.GetType().Name}' & '{storage.GetType().Name}' - {e.Message}");
                }
            }
            throw new InvalidOperationException($"No readable storage found for {key}");
        }

        private async UniTask<T> LoadAsync<T>(string key, IProgress<float>? progress, CancellationToken cancellationToken) where T : notnull
        {
            foreach (var (serializer, storage) in this.GetSerializerAndStorage(typeof(T)))
            {
                if (storage is not IReadableStorage readableStorage) continue;
                if (!await readableStorage.ContainsAsync(key, cancellationToken: cancellationToken)) continue;
                try
                {
                    var rawData = await readableStorage.ReadAsync(key, serializer.RawDataType, progress, cancellationToken);
                    var savedData = await serializer.DeserializeAsync<T>(rawData, cancellationToken);
                    this.logger.Debug($"Loaded {key} with '{serializer.GetType().Name}' & '{storage.GetType().Name}'");
                    return savedData;
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException($"Failed to load {key} with '{serializer.GetType().Name}' & '{storage.GetType().Name}' - {e.Message}");
                }
            }
            throw new InvalidOperationException($"No readable storage found for {key}");
        }

        private async UniTask SaveAsync(string key, Type type, object data, IProgress<float>? progress, CancellationToken cancellationToken)
        {
            foreach (var (serializer, storage) in this.GetSerializerAndStorage(type))
            {
                if (storage is not IWritableStorage writableStorage) continue;
                try
                {
                    var rawData = await serializer.SerializeAsync(type, data, cancellationToken);
                    await writableStorage.WriteAsync(key, rawData, progress, cancellationToken);
                    this.logger.Debug($"Saved {key} with '{serializer.GetType().Name}' & '{storage.GetType().Name}'");
                    return;
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException($"Failed to save {key} with '{serializer.GetType().Name}' & '{storage.GetType().Name}' - {e.Message}");
                }
            }
            throw new InvalidOperationException($"No writable storage found for {key}");
        }

        private async UniTask SaveAsync<T>(string key, T data, IProgress<float>? progress, CancellationToken cancellationToken) where T : notnull
        {
            foreach (var (serializer, storage) in this.GetSerializerAndStorage(typeof(T)))
            {
                if (storage is not IWritableStorage writableStorage) continue;
                try
                {
                    var rawData = await serializer.SerializeAsync(data, cancellationToken);
                    await writableStorage.WriteAsync(key, rawData, progress, cancellationToken);
                    this.logger.Debug($"Saved {key} with '{serializer.GetType().Name}' & '{storage.GetType().Name}'");
                    return;
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException($"Failed to save {key} with '{serializer.GetType().Name}' & '{storage.GetType().Name}' - {e.Message}");
                }
            }
            throw new InvalidOperationException($"No writable storage found for {key}");
        }

        private void Flush()
        {
            foreach (var storage in this.storages.OfType<IWritableStorage>())
            {
                storage.Flush();
                this.logger.Debug($"Flushed {storage.GetType().Name}");
            }
        }

        private IReadOnlyList<(ISerializer, IStorage)> GetSerializerAndStorage(Type type)
        {
            return this.serializerAndStorageCache.GetOrAdd(
                type,
                static state =>
                {
                    var (@this, type) = state;
                    var result = IterTools.Product(
                            @this.serializers.Where(static (serializer, type) => serializer.CanSerialize(type), type),
                            @this.storages.Where(static (storage, type) => typeof(IWritableData).IsAssignableFrom(type) == storage is IWritableStorage, type)
                        )
                        .Where(static (serializer, storage) => storage.CanStore(serializer.RawDataType))
                        .Reverse()
                        .ToArray();
                    if (result.Length is 0) throw new KeyNotFoundException($"No serializer or storage found for {type.Name}");
                    return result;
                },
                (@this: this, type)
            );
        }
    }
}