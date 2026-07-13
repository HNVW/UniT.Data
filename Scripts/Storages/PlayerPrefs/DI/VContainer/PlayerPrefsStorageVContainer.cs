#nullable enable
namespace UniT.Data.Storages.DI
{
    using VContainer;

    public static class PlayerPrefsStorageVContainer
    {
        public static void RegisterPlayerPrefsStorage(this IContainerBuilder builder)
        {
            builder.Register<PlayerPrefsStorage>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}