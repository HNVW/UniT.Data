#nullable enable
namespace UniT.Data.DI
{
    using InternalDI;
    using MemoryPack;
    using MemoryPackSerializer = MemoryPackSerializer;

    public static class MemoryPackSerializerInternalDI
    {
        public static void AddMemoryPackSerializer(this DependencyContainer container)
        {
            container.AddMemoryPackSerializer(MemoryPackSerializerOptions.Default);
        }

        public static void AddMemoryPackSerializer(this DependencyContainer container, MemoryPackSerializerOptions options)
        {
            container.Add(options);
            container.AddInterfacesAndSelf<MemoryPackSerializer>();
        }
    }
}