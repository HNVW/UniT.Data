#nullable enable
namespace UniT.Data.DI
{
    using VContainer;

    public static class DataManagerVContainer
    {
        public static void RegisterDataManager(this IContainerBuilder builder)
        {
            builder.Register<DataManager>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}