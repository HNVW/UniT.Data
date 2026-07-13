#nullable enable
namespace UniT.Data.Serializers.DI
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