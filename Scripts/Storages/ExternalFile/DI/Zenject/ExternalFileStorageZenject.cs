#nullable enable
namespace UniT.Data.Storages.ExternalFile.DI
{
    using Zenject;

    public static class ExternalFileStorageZenject
    {
        public static void BindExternalFileStorage<T>(this DiContainer container) where T : IExternalFileStorageVersionProvider
        {
            container.BindInterfacesTo<T>().AsSingle();
            container.BindInterfacesAndSelfTo<ExternalFileStorage>().AsSingle();
        }
    }
}