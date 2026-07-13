#nullable enable
namespace UniT.Data.Serializers.DI
{
    using InternalDI;

    public static class ProtobufSerializerInternalDI
    {
        public static void AddProtobufSerializer(this DependencyContainer container)
        {
            container.AddInterfacesAndSelf<ProtobufSerializer>();
        }
    }
}