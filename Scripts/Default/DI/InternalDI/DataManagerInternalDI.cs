#nullable enable
namespace UniT.Data.DI
{
    using InternalDI;

    public static class DataManagerInternalDI
    {
        public static void AddDataManager(this DependencyContainer container)
        {
            container.AddInterfaces<DataManager>();
        }
    }
}