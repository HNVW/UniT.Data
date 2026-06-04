#nullable enable
namespace UniT.Data.Storages.External.DI
{
    using Zenject;

    public static class ExternalStoragesZenject
    {
        public static void BindExternalStorages<T>(this DiContainer container) where T : IExternalFileVersionProvider
        {
            container.BindInterfacesTo<T>().AsSingle();
            container.BindInterfacesTo<ExternalFileVersionManager>().AsSingle();
            container.BindInterfacesTo<ExternalBinaryStorage>().AsSingle();
            container.BindInterfacesTo<ExternalTextStorage>().AsSingle();
        }
    }
}