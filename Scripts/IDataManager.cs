#nullable enable
namespace UniT.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UniT.Extensions;
    #if UNIT_UNITASK
    using System.Threading;
    using Cysharp.Threading.Tasks;
    #else
    using System.Collections;
    #endif

    public interface IDataManager
    {
        #region Sync

        public object[] Load(string[] keys, Type[] types, bool cache = true);

        public void Update(string[] keys, object[] datas);

        public void Unload(string[] keys);

        public void Save(string[] keys);

        public void Flush(string[] keys);

        public void SaveAll();

        public void FlushAll();

        #region Shortcuts

        #region Multiple

        #region Explicit Key

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SaveAndFlush(string[] keys)
        {
            this.Save(keys);
            this.Flush(keys);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SaveAndFlushAll()
        {
            this.SaveAll();
            this.FlushAll();
        }

        #endregion

        #region Implicit Key

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object[] Load(Type[] types, bool cache = true) => this.Load(GetKeys(types), types, cache);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update(object[] datas) => this.Update(GetKeys(datas.Select(data => data.GetType()).ToArray()), datas);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Unload(Type[] types) => this.Unload(GetKeys(types));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Save(Type[] types) => this.Save(GetKeys(types));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Flush(Type[] types) => this.Flush(GetKeys(types));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SaveAndFlush(Type[] types) => this.SaveAndFlush(GetKeys(types));

        #endregion

        #endregion

        #region Single

        #region Explicit Key

        #region Non-Generic

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object Load(string key, Type type, bool cache = true) => this.Load(new[] { key }, new[] { type }, cache)[0];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update(string key, object data) => this.Update(new[] { key }, new[] { data });

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Unload(string key) => this.Unload(new[] { key });

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Save(string key) => this.Save(new[] { key });

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Flush(string key) => this.Flush(new[] { key });

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SaveAndFlush(string key) => this.SaveAndFlush(new[] { key });

        #endregion

        #region Generic

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Load<T>(string key, bool cache = true) => (T)this.Load(key, typeof(T), cache);

        #endregion

        #endregion

        #region Implicit Key

        #region Non-Generic

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object Load(Type type, bool cache = true) => this.Load(type.GetKey(), type, cache);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update(object data) => this.Update(data.GetType().GetKey(), data);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Unload(Type type) => this.Unload(type.GetKey());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Save(Type type) => this.Save(type.GetKey());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Flush(Type type) => this.Flush(type.GetKey());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SaveAndFlush(Type type) => this.SaveAndFlush(type.GetKey());

        #endregion

        #region Generic

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Load<T>(bool cache = true) => (T)this.Load(typeof(T).GetKey(), typeof(T), cache);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update<T>(T data) where T : notnull => this.Update(typeof(T).GetKey(), data);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Unload<T>() => this.Unload(typeof(T).GetKey());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Save<T>() => this.Save(typeof(T).GetKey());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Flush<T>() => this.Flush(typeof(T).GetKey());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SaveAndFlush<T>() => this.SaveAndFlush(typeof(T).GetKey());

        #endregion

        #endregion

        #endregion

        #endregion

        #endregion

        #region Async

        #if UNIT_UNITASK
        public UniTask<object[]> LoadAsync(string[] keys, Type[] types, bool cache = true, IProgress<float>? progress = null, CancellationToken cancellationToken = default);

        public UniTask SaveAsync(string[] keys, IProgress<float>? progress = null, CancellationToken cancellationToken = default);

        public UniTask FlushAsync(string[] keys, IProgress<float>? progress = null, CancellationToken cancellationToken = default);

        public UniTask SaveAllAsync(IProgress<float>? progress = null, CancellationToken cancellationToken = default);

        public UniTask FlushAllAsync(IProgress<float>? progress = null, CancellationToken cancellationToken = default);

        #region Shortcuts

        #region Multiple

        #region Explicit Key

        public async UniTask SaveAndFlushAsync(string[] keys, IProgress<float>? progress = null, CancellationToken cancellationToken = default)
        {
            var subProgresses = progress.CreateSubProgresses(2).ToArray();
            await this.SaveAsync(keys, subProgresses[0], cancellationToken);
            await this.FlushAsync(keys, subProgresses[1], cancellationToken);
        }

        public async UniTask SaveAndFlushAllAsync(IProgress<float>? progress = null, CancellationToken cancellationToken = default)
        {
            var subProgresses = progress.CreateSubProgresses(2).ToArray();
            await this.SaveAllAsync(subProgresses[0], cancellationToken);
            await this.FlushAllAsync(subProgresses[1], cancellationToken);
        }

        #endregion

        #region Implicit Key

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask<object[]> LoadAsync(Type[] types, bool cache = true, IProgress<float>? progress = null, CancellationToken cancellationToken = default) => this.LoadAsync(GetKeys(types), types, cache, progress, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask SaveAsync(Type[] types, IProgress<float>? progress = null, CancellationToken cancellationToken = default) => this.SaveAsync(GetKeys(types), progress, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask FlushAsync(Type[] types, IProgress<float>? progress = null, CancellationToken cancellationToken = default) => this.FlushAsync(GetKeys(types), progress, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask SaveAndFlushAsync(Type[] types, IProgress<float>? progress = null, CancellationToken cancellationToken = default) => this.SaveAndFlushAsync(GetKeys(types), progress, cancellationToken);

        #endregion

        #endregion

        #region Single

        #region Explicit Key

        #region Non-Generic

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask<object> LoadAsync(string key, Type type, bool cache = true, IProgress<float>? progress = null, CancellationToken cancellationToken = default) => this.LoadAsync(new[] { key }, new[] { type }, cache, progress, cancellationToken).ContinueWith(datas => datas[0]);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask SaveAsync(string key, IProgress<float>? progress = null, CancellationToken cancellationToken = default) => this.SaveAsync(new[] { key }, progress, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask FlushAsync(string key, IProgress<float>? progress = null, CancellationToken cancellationToken = default) => this.FlushAsync(new[] { key }, progress, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask SaveAndFlushAsync(string key, IProgress<float>? progress = null, CancellationToken cancellationToken = default) => this.SaveAndFlushAsync(new[] { key }, progress, cancellationToken);

        #endregion

        #region Generic

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask<T> LoadAsync<T>(string key, bool cache = true, IProgress<float>? progress = null, CancellationToken cancellationToken = default) => this.LoadAsync(key, typeof(T), cache, progress, cancellationToken).ContinueWith(data => (T)data);

        #endregion

        #endregion

        #region Implicit Key

        #region Non-Generic

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask<object> LoadAsync(Type type, bool cache = true, IProgress<float>? progress = null, CancellationToken cancellationToken = default) => this.LoadAsync(type.GetKey(), type, cache, progress, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask SaveAsync(Type type, IProgress<float>? progress = null, CancellationToken cancellationToken = default) => this.SaveAsync(type.GetKey(), progress, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask FlushAsync(Type type, IProgress<float>? progress = null, CancellationToken cancellationToken = default) => this.FlushAsync(type.GetKey(), progress, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask SaveAndFlushAsync(Type type, IProgress<float>? progress = null, CancellationToken cancellationToken = default) => this.SaveAndFlushAsync(type.GetKey(), progress, cancellationToken);

        #endregion

        #region Generic

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask<T> LoadAsync<T>(bool cache = true, IProgress<float>? progress = null, CancellationToken cancellationToken = default) => this.LoadAsync(typeof(T).GetKey(), typeof(T), cache, progress, cancellationToken).ContinueWith(data => (T)data);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask SaveAsync<T>(IProgress<float>? progress = null, CancellationToken cancellationToken = default) => this.SaveAsync(typeof(T).GetKey(), progress, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask FlushAsync<T>(IProgress<float>? progress = null, CancellationToken cancellationToken = default) => this.FlushAsync(typeof(T).GetKey(), progress, cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask SaveAndFlushAsync<T>(IProgress<float>? progress = null, CancellationToken cancellationToken = default) => this.SaveAndFlushAsync(typeof(T).GetKey(), progress, cancellationToken);

        #endregion

        #endregion

        #endregion

        #endregion

        #else
        public IEnumerator LoadAsync(string[] keys, Type[] types, Action<object[]> callback, bool cache = true, IProgress<float>? progress = null);

        public IEnumerator SaveAsync(string[] keys, Action? callback = null, IProgress<float>? progress = null);

        public IEnumerator FlushAsync(string[] keys, Action? callback = null, IProgress<float>? progress = null);

        public IEnumerator SaveAllAsync(Action? callback = null, IProgress<float>? progress = null);

        public IEnumerator FlushAllAsync(Action? callback = null, IProgress<float>? progress = null);

        #region Shortcuts

        #region Multiple

        #region Explicit Key

        public IEnumerator SaveAndFlushAsync(string[] keys, Action? callback = null, IProgress<float>? progress = null)
        {
            var subProgresses = progress.CreateSubProgresses(2).ToArray();
            yield return this.SaveAsync(keys, progress: subProgresses[0]);
            yield return this.FlushAsync(keys, progress: subProgresses[1]);
            callback?.Invoke();
        }

        public IEnumerator SaveAndFlushAllAsync(Action? callback = null, IProgress<float>? progress = null)
        {
            var subProgresses = progress.CreateSubProgresses(2).ToArray();
            yield return this.SaveAllAsync(progress: subProgresses[0]);
            yield return this.FlushAllAsync(progress: subProgresses[1]);
            callback?.Invoke();
        }

        #endregion

        #region Implicit Key

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator LoadAsync(Type[] types, Action<object[]> callback, bool cache = true, IProgress<float>? progress = null) => this.LoadAsync(GetKeys(types), types, callback, cache, progress);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator SaveAsync(Type[] types, Action? callback = null, IProgress<float>? progress = null) => this.SaveAsync(GetKeys(types), callback, progress);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator FlushAsync(Type[] types, Action? callback = null, IProgress<float>? progress = null) => this.FlushAsync(GetKeys(types), callback, progress);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator SaveAndFlushAsync(Type[] types, Action? callback = null, IProgress<float>? progress = null) => this.SaveAndFlushAsync(GetKeys(types), callback, progress);

        #endregion

        #endregion

        #region Single

        #region Explicit Key

        #region Non-Generic

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator LoadAsync(string key, Type type, Action<object> callback, bool cache = true, IProgress<float>? progress = null) => this.LoadAsync(new[] { key }, new[] { type }, datas => callback(datas[0]), cache, progress);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator SaveAsync(string key, Action? callback = null, IProgress<float>? progress = null) => this.SaveAsync(new[] { key }, callback, progress);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator FlushAsync(string key, Action? callback = null, IProgress<float>? progress = null) => this.FlushAsync(new[] { key }, callback, progress);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator SaveAndFlushAsync(string key, Action? callback = null, IProgress<float>? progress = null) => this.SaveAndFlushAsync(new[] { key }, callback, progress);

        #endregion

        #region Generic

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator LoadAsync<T>(string key, Action<T> callback, bool cache = true, IProgress<float>? progress = null) => this.LoadAsync(key, typeof(T), data => callback((T)data), cache, progress);

        #endregion

        #endregion

        #region Implicit Key

        #region Non-Generic

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator LoadAsync(Type type, Action<object> callback, bool cache = true, IProgress<float>? progress = null) => this.LoadAsync(type.GetKey(), type, callback, cache, progress);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator SaveAsync(Type type, Action? callback = null, IProgress<float>? progress = null) => this.SaveAsync(type.GetKey(), callback, progress);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator FlushAsync(Type type, Action? callback = null, IProgress<float>? progress = null) => this.FlushAsync(type.GetKey(), callback, progress);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator SaveAndFlushAsync(Type type, Action? callback = null, IProgress<float>? progress = null) => this.SaveAndFlushAsync(type.GetKey(), callback, progress);

        #endregion

        #region Generic

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator LoadAsync<T>(Action<T> callback, bool cache = true, IProgress<float>? progress = null) => this.LoadAsync(typeof(T).GetKey(), typeof(T), data => callback((T)data), cache, progress);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator SaveAsync<T>(Action? callback = null, IProgress<float>? progress = null) => this.SaveAsync(typeof(T).GetKey(), callback, progress);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator FlushAsync<T>(Action? callback = null, IProgress<float>? progress = null) => this.FlushAsync(typeof(T).GetKey(), callback, progress);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator SaveAndFlushAsync<T>(Action? callback = null, IProgress<float>? progress = null) => this.SaveAndFlushAsync(typeof(T).GetKey(), callback, progress);

        #endregion

        #endregion

        #endregion

        #endregion

        #endif

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string[] GetKeys(IEnumerable<Type> types) => types.Select(type => type.GetKey()).ToArray();
    }
}