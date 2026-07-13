#nullable enable
namespace UniT.Data.Serializers.DI
{
    using Zenject;

    public static class ProtobufSerializerZenject
    {
        public static void BindProtobufSerializer(this DiContainer container)
        {
            container.BindInterfacesAndSelfTo<ProtobufSerializer>().AsSingle();
        }
    }
}