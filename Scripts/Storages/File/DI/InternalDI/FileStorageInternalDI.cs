#nullable enable
namespace UniT.Data.Storages.File.DI
{
    using InternalDI;

    public static class FileStorageInternalDI
    {
        public static void AddFileStorage(this DependencyContainer container)
        {
            container.AddInterfacesAndSelf<FileStorage>();
        }
    }
}