#nullable enable
namespace UniT.Data
{
    using System;
    using System.Threading;
    using Cysharp.Threading.Tasks;
    using Extensions;

    public interface IDataManager
    {
        public UniTask<bool> ContainsAsync(string key, Type type, IProgress<float>? progress = null, CancellationToken cancellationToken = default);

        public UniTask<bool> ContainsAsync<T>(string key, IProgress<float>? progress = null, CancellationToken cancellationToken = default) where T : notnull;

        public UniTask<object> LoadAsync(string key, Type type, bool cache = false, IProgress<float>? progress = null, CancellationToken cancellationToken = default);

        public UniTask<T> LoadAsync<T>(string key, bool cache = false, IProgress<float>? progress = null, CancellationToken cancellationToken = default) where T : notnull;

        public UniTask SaveAsync(string key, IProgress<float>? progress = null, CancellationToken cancellationToken = default);

        public UniTask SaveAsync(string key, Type type, object data, bool cache = false, IProgress<float>? progress = null, CancellationToken cancellationToken = default);

        public UniTask SaveAsync<T>(string key, T data, bool cache = false, IProgress<float>? progress = null, CancellationToken cancellationToken = default) where T : notnull;

        public UniTask SaveAllAsync(IProgress<float>? progress = null, CancellationToken cancellationToken = default);

        public void Unload(string key);

        public void Flush();

        #region Implicit Key

        public UniTask<bool> ContainsAsync(Type type, IProgress<float>? progress = null, CancellationToken cancellationToken = default) => this.ContainsAsync(type.GetKey(), type, progress, cancellationToken);

        public UniTask<object> LoadAsync(Type type, bool cache = false, IProgress<float>? progress = null, CancellationToken cancellationToken = default) => this.LoadAsync(type.GetKey(), type, cache, progress, cancellationToken);

        public UniTask SaveAsync(Type type, IProgress<float>? progress = null, CancellationToken cancellationToken = default) => this.SaveAsync(type.GetKey(), progress, cancellationToken);

        public UniTask SaveAsync(Type type, object data, bool cache = false, IProgress<float>? progress = null, CancellationToken cancellationToken = default) => this.SaveAsync(type.GetKey(), type, data, cache, progress, cancellationToken);

        public void Unload(Type type) => this.Unload(type.GetKey());

        public UniTask<bool> ContainsAsync<T>(IProgress<float>? progress = null, CancellationToken cancellationToken = default) where T : notnull => this.ContainsAsync<T>(typeof(T).GetKey(), progress, cancellationToken);

        public UniTask<T> LoadAsync<T>(bool cache = false, IProgress<float>? progress = null, CancellationToken cancellationToken = default) where T : notnull => this.LoadAsync<T>(typeof(T).GetKey(), cache, progress, cancellationToken);

        public UniTask SaveAsync<T>(IProgress<float>? progress = null, CancellationToken cancellationToken = default) where T : notnull => this.SaveAsync(typeof(T).GetKey(), progress, cancellationToken);

        public UniTask SaveAsync<T>(T data, bool cache = false, IProgress<float>? progress = null, CancellationToken cancellationToken = default) where T : notnull => this.SaveAsync(typeof(T).GetKey(), data, cache, progress, cancellationToken);

        public void Unload<T>() where T : notnull => this.Unload(typeof(T).GetKey());

        #endregion
    }
}