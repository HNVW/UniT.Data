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

        public UniTask SaveAsync(string key, IProgress<float>? progress = null, CancellationToken cancellationToken = default);

        public UniTask SaveAllAsync(IProgress<float>? progress = null, CancellationToken cancellationToken = default);

        public void Update(string key, object data);

        public void Unload(string key);

        public void Flush();

        public UniTask<T> LoadAsync<T>(string key, bool cache = true, IProgress<float>? progress = null, CancellationToken cancellationToken = default) where T : notnull => this.LoadAsync(key, typeof(T), cache, progress, cancellationToken).ContinueWith(static data => (T)data);

        #region Implicit Key

        public UniTask<object> LoadAsync(Type type, bool cache = true, IProgress<float>? progress = null, CancellationToken cancellationToken = default) => this.LoadAsync(type.GetKey(), type, cache, progress, cancellationToken);

        public void Update(object data) => this.Update(data.GetType().GetKey(), data);

        public UniTask<T> LoadAsync<T>(bool cache = true, IProgress<float>? progress = null, CancellationToken cancellationToken = default) where T : notnull => this.LoadAsync(typeof(T).GetKey(), typeof(T), cache, progress, cancellationToken).ContinueWith(static data => (T)data);

        public UniTask SaveAsync<T>(IProgress<float>? progress = null, CancellationToken cancellationToken = default) where T : notnull => this.SaveAsync(typeof(T).GetKey(), progress, cancellationToken);

        public void Update<T>(T data) where T : notnull => this.Update(typeof(T).GetKey(), data);

        public void Unload<T>() where T : notnull => this.Unload(typeof(T).GetKey());

        #endregion
    }
}