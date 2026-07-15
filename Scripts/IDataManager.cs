#nullable enable
namespace UniT.Data
{
    using System;
    using System.Threading;
    using Cysharp.Threading.Tasks;
    using Extensions;

    public interface IDataManager
    {
        public UniTask<object> LoadAsync(string key, Type type, IProgress<float>? progress = null, CancellationToken cancellationToken = default);

        public UniTask SaveAsync(string key, object data, IProgress<float>? progress = null, CancellationToken cancellationToken = default);

        public void Flush();

        public UniTask<T> LoadAsync<T>(string key, IProgress<float>? progress = null, CancellationToken cancellationToken = default) where T : notnull => this.LoadAsync(key, typeof(T), progress, cancellationToken).ContinueWith(static data => (T)data);

        #region Implicit Key

        public UniTask<object> LoadAsync(Type type, IProgress<float>? progress = null, CancellationToken cancellationToken = default) => this.LoadAsync(type.GetKey(), type, progress, cancellationToken);

        public UniTask SaveAsync(object data, IProgress<float>? progress = null, CancellationToken cancellationToken = default) => this.SaveAsync(data.GetType().GetKey(), data, progress, cancellationToken);

        public UniTask<T> LoadAsync<T>(IProgress<float>? progress = null, CancellationToken cancellationToken = default) where T : notnull => this.LoadAsync(typeof(T).GetKey(), typeof(T), progress, cancellationToken).ContinueWith(static data => (T)data);

        #endregion
    }
}