#nullable enable
namespace UniT.Data.Serializers.DI
{
    using InternalDI;

    public static class UnitySerializerInternalDI
    {
        public static void AddUnitySerializer(this DependencyContainer container)
        {
            container.AddInterfacesAndSelf<UnitySerializer>();
        }
    }
}