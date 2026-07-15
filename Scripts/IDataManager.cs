#nullable enable
namespace UniT.Data
{
    using System;
    using System.Threading;
    using Cysharp.Threading.Tasks;
    using Extensions;

    public interface IDataManager
    {
        public UniTask<object> LoadAsync(string key, Type type, bool cache = true, IProgress<float>? progress = null, CancellationToken cancellationToken = default);

        public void Update(string key, object data);

        public UniTask SaveAsync(string key, IProgress<float>? progress = null, CancellationToken cancellationToken = default);

        public void Flush();

        public UniTask<T> LoadAsync<T>(string key, bool cache = true, IProgress<float>? progress = null, CancellationToken cancellationToken = default) where T : notnull => this.LoadAsync(key, typeof(T), cache, progress, cancellationToken).ContinueWith(static data => (T)data);

        #region Implicit Key

        public UniTask<object> LoadAsync(Type type, bool cache = true, IProgress<float>? progress = null, CancellationToken cancellationToken = default) => this.LoadAsync(type.GetKey(), type, cache, progress, cancellationToken);

        public UniTask SaveAsync<T>(IProgress<float>? progress = null, CancellationToken cancellationToken = default) => this.SaveAsync(typeof(T).GetKey(), progress, cancellationToken);

        public UniTask<T> LoadAsync<T>(bool cache = true, IProgress<float>? progress = null, CancellationToken cancellationToken = default) where T : notnull => this.LoadAsync(typeof(T).GetKey(), typeof(T), cache, progress, cancellationToken).ContinueWith(static data => (T)data);

        #endregion
    }
}