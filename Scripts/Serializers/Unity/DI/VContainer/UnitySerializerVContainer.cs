#nullable enable
namespace UniT.Data.DI
{
    using VContainer;

    public static class UnitySerializerVContainer
    {
        public static void RegisterUnitySerializer(this IContainerBuilder builder)
        {
            builder.Register<UnitySerializer>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}