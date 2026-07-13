#nullable enable
namespace UniT.Data.Serializers.Default.DI
{
    using InternalDI;

    public static class DefaultSerializerInternalDI
    {
        public static void AddDefaultSerializer(this DependencyContainer container)
        {
            container.AddInterfacesAndSelf<DefaultSerializer>();
        }
    }
}