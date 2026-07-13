#nullable enable
namespace UniT.Data.Storages.ExternalFile.DI
{
    using InternalDI;

    public static class ExternalFileStorageInternalDI
    {
        public static void AddExternalFileStorage<T>(this DependencyContainer container) where T : IExternalFileStorageVersionProvider
        {
            container.AddInterfaces<T>();
            container.AddInterfacesAndSelf<ExternalFileStorage>();
        }
    }
}