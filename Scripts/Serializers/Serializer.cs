#nullable enable
namespace UniT.Data
{
    using System;
    using System.Threading;
    using Cysharp.Threading.Tasks;

    public abstract class Serializer<TRawData, TData> : ISerializer where TRawData : notnull where TData : notnull
    {
        Type ISerializer.RawDataType => typeof(TRawData);

        bool ISerializer.CanSerialize(Type type) => this.CanSerialize(type);

        async UniTask<object> ISerializer.DeserializeAsync(Type type, object rawData, CancellationToken cancellationToken)
        {
            try
            {
                return await this.DeserializeAsync(type, (TRawData)rawData, cancellationToken);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Failed to deserialize '{rawData}' to '{type.Name}' with '{this.GetType().Name}' - {e.Message}");
            }
        }

        async UniTask<object> ISerializer.SerializeAsync(Type type, object data, CancellationToken cancellationToken)
        {
            try
            {
                return await this.SerializeAsync(type, (TData)data, cancellationToken);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Failed to serialize '{type.Name}' '{data}' with '{this.GetType().Name}' - {e.Message}");
            }
        }

        protected virtual bool CanSerialize(Type type) => typeof(TData).IsAssignableFrom(type);

        public abstract TData Deserialize(Type type, TRawData rawData);

        public abstract TRawData Serialize(Type type, TData data);

        public virtual T Deserialize<T>(TRawData rawData) where T : TData => (T)this.Deserialize(typeof(T), rawData);

        public virtual TRawData Serialize<T>(T data) where T : TData => this.Serialize(typeof(T), data);

        public virtual UniTask<TData> DeserializeAsync(Type type, TRawData rawData, CancellationToken cancellationToken = default)
        {
#if !UNITY_WEBGL
            return UniTask.RunOnThreadPool(() => this.Deserialize(type, rawData), cancellationToken: cancellationToken);
#else
            return UniTask.FromResult(this.Deserialize(type, rawData));
#endif
        }

        public virtual UniTask<TRawData> SerializeAsync(Type type, TData data, CancellationToken cancellationToken = default)
        {
            return UniTask.FromResult(this.Serialize(type, data));
        }

        public virtual UniTask<T> DeserializeAsync<T>(TRawData rawData) where T : TData => this.DeserializeAsync(typeof(T), rawData).ContinueWith(data => (T)data);

        public virtual UniTask<TRawData> SerializeAsync<T>(T data) where T : TData => this.SerializeAsync(typeof(T), data);
    }
}