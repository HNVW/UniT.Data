#nullable enable
namespace UniT.Data.DI
{
    using MessagePack;
    using Zenject;
    using MessagePackSerializer = MessagePackSerializer;

    public static class MessagePackSerializerZenject
    {
        public static void BindMessagePackSerializer(this DiContainer container)
        {
            container.BindMessagePackSerializer(MessagePackSerializerOptions.Standard);
        }

        public static void BindMessagePackSerializer(this DiContainer container, MessagePackSerializerOptions options)
        {
            container.BindInstance(options);
            container.BindInterfacesAndSelfTo<MessagePackSerializer>().AsSingle();
        }
    }
}