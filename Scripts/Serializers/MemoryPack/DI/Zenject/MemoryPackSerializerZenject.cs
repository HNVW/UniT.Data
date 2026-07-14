#nullable enable
namespace UniT.Data.DI
{
    using MemoryPack;
    using Zenject;
    using MemoryPackSerializer = MemoryPackSerializer;

    public static class MemoryPackSerializerZenject
    {
        public static void BindMemoryPackSerializer(this DiContainer container)
        {
            container.BindMemoryPackSerializer(MemoryPackSerializerOptions.Default);
        }

        public static void BindMemoryPackSerializer(this DiContainer container, MemoryPackSerializerOptions options)
        {
            container.BindInstance(options);
            container.BindInterfacesAndSelfTo<MemoryPackSerializer>().AsSingle();
        }
    }
}