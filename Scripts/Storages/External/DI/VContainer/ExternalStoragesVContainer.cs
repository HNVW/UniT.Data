#nullable enable
namespace UniT.Data.Storages.External.DI
{
    using VContainer;

    public static class ExternalStoragesVContainer
    {
        public static void RegisterExternalStorages<T>(this IContainerBuilder builder) where T : IExternalFileVersionProvider
        {
            builder.Register<T>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ExternalFileVersionManager>(Lifetime.Singleton);
            builder.Register<ExternalBinaryStorage>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ExternalTextStorage>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}