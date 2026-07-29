#nullable enable
namespace UniT.Data
{
    using System;
    using System.Threading;
    using Cysharp.Threading.Tasks;
    using Extensions;
    using UniTaskExtensions = Extensions.UniTaskExtensions;

    public abstract class Serializer : ISerializer
    {
        Type ISerializer.RawDataType => this.RawDataType;

        bool ISerializer.CanSerialize(Type type) => this.CanSerialize(type);

        public virtual async UniTask<object> DeserializeAsync(Type type, object rawData, CancellationToken cancellationToken = default)
        {
            try
            {
                return await UniTaskExtensions.RunOnThreadPool(static state => state.@this.Deserialize(state.type, state.rawData), (@this: this, type, rawData), cancellationToken);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Failed to deserialize '{rawData.ToString().Truncate(64)}' to '{type.Name}' with '{this.GetType().Name}' - {e.Message}");
            }
        }

        public virtual UniTask<object> SerializeAsync(Type type, object data, CancellationToken cancellationToken = default)
        {
            try
            {
                return UniTask.FromResult(this.Serialize(type, data));
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Failed to serialize '{type.Name}' '{data}' with '{this.GetType().Name}' - {e.Message}");
            }
        }

        public virtual async UniTask<T> DeserializeAsync<T>(object rawData, CancellationToken cancellationToken = default) where T : notnull
        {
            try
            {
                return await UniTaskExtensions.RunOnThreadPool(static state => state.@this.Deserialize<T>(state.rawData), (@this: this, rawData), cancellationToken);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Failed to deserialize '{rawData.ToString().Truncate(64)}' to '{typeof(T).Name}' with '{this.GetType().Name}' - {e.Message}");
            }
        }

        public virtual UniTask<object> SerializeAsync<T>(T data, CancellationToken cancellationToken = default) where T : notnull
        {
            try
            {
                return UniTask.FromResult(this.Serialize(data));
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Failed to serialize '{typeof(T).Name}' '{data}' with '{this.GetType().Name}' - {e.Message}");
            }
        }

        protected abstract Type RawDataType { get; }

        protected abstract bool CanSerialize(Type type);

        public abstract object Deserialize(Type type, object rawData);

        public abstract object Serialize(Type type, object data);

        public virtual T Deserialize<T>(object rawData) where T : notnull => (T)this.Deserialize(typeof(T), rawData);

        public virtual object Serialize<T>(T data) where T : notnull => this.Serialize(typeof(T), data);
    }
}