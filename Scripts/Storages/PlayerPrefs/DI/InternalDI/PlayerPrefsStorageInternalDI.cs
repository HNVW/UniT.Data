#nullable enable
namespace UniT.Data.Storages.PlayerPrefs.DI
{
    using InternalDI;

    public static class PlayerPrefsStorageInternalDI
    {
        public static void AddPlayerPrefsStorage(this DependencyContainer container)
        {
            container.AddInterfacesAndSelf<PlayerPrefsStorage>();
        }
    }
}