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

    public sealed class DataManager : IDataManager
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

        UniTask<object> IDataManager.LoadAsync(string key, Type type, bool cache, IProgress<float>? progress, CancellationToken cancellationToken)
        {
            return cache
                ? this.cache.GetOrAddAsync(key, LoadAsync)
                : LoadAsync();

            async UniTask<object> LoadAsync()
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
                var newData = type.GetEmptyConstructor()();
                this.logger.Debug($"Instantiated {key}");
                return newData;
            }
        }

        void IDataManager.Update(string key, object data)
        {
            this.cache[key] = data;
        }

        async UniTask IDataManager.SaveAsync(string key, IProgress<float>? progress, CancellationToken cancellationToken)
        {
            var data = this.cache[key];
            var type = data.GetType();
            foreach (var (serializer, storage) in this.GetSerializerAndStorage(type))
            {
                if (storage is not IWritableStorage writableStorage) continue;
                try
                {
                    var rawData = await serializer.SerializeAsync(type, data, cancellationToken);
                    await writableStorage.WriteAsync(key, rawData, serializer.RawDataType, progress, cancellationToken);
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

        void IDataManager.Flush()
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
                    var result = IterTools.Product(
                            state.@this.serializers.Where(static (serializer, type) => serializer.CanSerialize(type), state.type),
                            state.@this.storages.Where(static (storage, type) => typeof(IWritableData).IsAssignableFrom(type) == storage is IWritableStorage, state.type)
                        )
                        .Where(static (serializer, storage) => storage.CanStore(serializer.RawDataType))
                        .Reverse()
                        .ToArray();
                    if (result.Length is 0) throw new KeyNotFoundException($"No serializer or storage found for {state.type.Name}");
                    return result;
                },
                (@this: this, type)
            );
        }
    }
}