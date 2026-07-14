#nullable enable
namespace UniT.Data.DI
{
    using VContainer;

    public static class DefaultSerializerVContainer
    {
        public static void RegisterDefaultSerializer(this IContainerBuilder builder)
        {
            builder.Register<DefaultSerializer>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}