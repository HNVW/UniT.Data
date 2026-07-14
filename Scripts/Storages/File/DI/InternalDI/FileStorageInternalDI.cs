#nullable enable
namespace UniT.Data.DI
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