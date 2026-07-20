#nullable enable
namespace UniT.Data
{
    using System;
    using System.Threading;
    using Cysharp.Threading.Tasks;

    public interface IWritableStorage : IStorage
    {
        public UniTask WriteAsync(string key, object value, IProgress<float>? progress = null, CancellationToken cancellationToken = default);

        public void Flush();
    }
}