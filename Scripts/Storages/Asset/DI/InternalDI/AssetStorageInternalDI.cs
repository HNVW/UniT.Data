#nullable enable
namespace UniT.Data.DI
{
    using InternalDI;

    public static class AssetStorageInternalDI
    {
        public static void AddAssetStorage(this DependencyContainer container)
        {
            container.AddInterfacesAndSelf<AssetStorage>();
        }
    }
}