#nullable enable
namespace UniT.Data.DI
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