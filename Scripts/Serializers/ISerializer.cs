#nullable enable
namespace UniT.Data
{
    using System;
    using System.Threading;
    using Cysharp.Threading.Tasks;

    public interface ISerializer
    {
        public Type RawDataType { get; }

        public bool CanSerialize(Type type);

        public UniTask<object> DeserializeAsync(Type type, object rawData, CancellationToken cancellationToken = default);

        public UniTask<object> SerializeAsync(Type type, object data, CancellationToken cancellationToken = default);

        public UniTask<T> DeserializeAsync<T>(object rawData, CancellationToken cancellationToken = default) where T : notnull;

        public UniTask<object> SerializeAsync<T>(T data, CancellationToken cancellationToken = default) where T : notnull;
    }
}