#nullable enable
namespace UniT.Data.Storages.File
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using Cysharp.Threading.Tasks;
    using UniT.Extensions;
    using UnityEngine;
    using UnityEngine.Scripting;

    public class FileTextStorage : Storage<string>, IFlushableStorage
    {
        private static readonly string PersistentDataPath = Application.persistentDataPath;

        private readonly HashSet<string> dirtyKeys = new();

        [Preserve]
        public FileTextStorage()
        {
        }

        protected override bool CanStore(Type type) => typeof(IWritableData).IsAssignableFrom(type);

        UniTask<bool> IReadableStorage.ContainsAsync(string key, IProgress<float>? progress, CancellationToken cancellationToken)
        {
            return UniTask.FromResult(File.Exists(GetPath(key)));
        }

        UniTask<object> IReadableStorage.ReadAsync(string key, IProgress<float>? progress, CancellationToken cancellationToken)
        {
            return File.ReadAllTextAsync(GetPath(key), cancellationToken).AsUniTask().ContinueWith(result => (object)result);
        }

        async UniTask IWritableStorage.WriteAsync(string key, object value, IProgress<float>? progress, CancellationToken cancellationToken)
        {
            var tempPath = GetTempPath(key);
            Directory.CreateDirectory(Path.GetDirectoryName(tempPath)!);
            await File.WriteAllTextAsync(tempPath, (string)value, cancellationToken).AsUniTask();
            this.dirtyKeys.Add(key);
        }

        UniTask IFlushableStorage.FlushAsync(IProgress<float>? progress, CancellationToken cancellationToken)
        {
            this.dirtyKeys.SafeForEach(static (key, dirtyKeys) =>
            {
                var tempPath = GetTempPath(key);
                var path     = GetPath(key);
                if (File.Exists(path))
                {
                    File.Replace(tempPath, path, null);
                }
                else
                {
                    File.Move(tempPath, path);
                }
                dirtyKeys.Remove(key);
            }, this.dirtyKeys);
            return UniTask.CompletedTask;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string GetPath(string key) => Path.Combine(PersistentDataPath, key);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string GetTempPath(string key) => GetPath(key) + ".tmp";
    }
}