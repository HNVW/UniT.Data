#nullable enable
namespace UniT.Data.Storages.External.DI
{
    using UniT.DI;

    public static class ExternalStoragesInternalDI
    {
        public static void AddExternalStorages<T>(this DependencyContainer container) where T : IExternalFileVersionProvider
        {
            container.AddInterfaces<T>();
            container.Add<ExternalFileVersionManager>();
            container.AddInterfaces<ExternalBinaryStorage>();
            container.AddInterfaces<ExternalTextStorage>();
        }
    }
}