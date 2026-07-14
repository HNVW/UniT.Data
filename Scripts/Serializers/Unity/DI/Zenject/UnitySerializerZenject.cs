#nullable enable
namespace UniT.Data.DI
{
    using Zenject;

    public static class UnitySerializerZenject
    {
        public static void BindUnitySerializer(this DiContainer container)
        {
            container.BindInterfacesAndSelfTo<UnitySerializer>().AsSingle();
        }
    }
}