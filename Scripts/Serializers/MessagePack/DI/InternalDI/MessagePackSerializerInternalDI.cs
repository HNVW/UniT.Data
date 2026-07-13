#nullable enable
namespace UniT.Data.Serializers.DI
{
    using InternalDI;
    using MessagePack;
    using MessagePackSerializer = MessagePackSerializer;

    public static class MessagePackSerializerInternalDI
    {
        public static void AddMessagePackSerializer(this DependencyContainer container)
        {
            container.AddMessagePackSerializer(MessagePackSerializerOptions.Standard);
        }

        public static void AddMessagePackSerializer(this DependencyContainer container, MessagePackSerializerOptions options)
        {
            container.Add(options);
            container.AddInterfacesAndSelf<MessagePackSerializer>();
        }
    }
}