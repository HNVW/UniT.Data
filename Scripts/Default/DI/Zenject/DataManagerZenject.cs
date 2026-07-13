#nullable enable
namespace UniT.Data.DI
{
    using Zenject;

    public static class DataManagerZenject
    {
        public static void BindDataManager(this DiContainer container)
        {
            container.BindInterfacesTo<DataManager>().AsSingle();
        }
    }
}