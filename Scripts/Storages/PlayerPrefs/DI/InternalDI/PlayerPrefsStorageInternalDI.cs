#nullable enable
namespace UniT.Data.Storages.DI
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