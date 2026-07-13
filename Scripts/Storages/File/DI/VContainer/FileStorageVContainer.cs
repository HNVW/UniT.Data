#nullable enable
namespace UniT.Data.Storages.DI
{
    using VContainer;

    public static class FileStorageVContainer
    {
        public static void RegisterFileStorage(this IContainerBuilder builder)
        {
            builder.Register<FileStorage>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}