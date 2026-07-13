#nullable enable
namespace UniT.Data.Storages.DI
{
    using VContainer;

    public static class ExternalFileStorageVContainer
    {
        public static void RegisterExternalFileStorage<T>(this IContainerBuilder builder) where T : IExternalFileStorageVersionProvider
        {
            builder.Register<T>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ExternalFileStorage>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}