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
    }
}