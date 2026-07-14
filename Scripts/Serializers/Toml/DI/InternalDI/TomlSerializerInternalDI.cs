#nullable enable
namespace UniT.Data.DI
{
    using InternalDI;
    using Tomlyn;
    using TomlSerializer = TomlSerializer;

    public static class TomlSerializerInternalDI
    {
        public static void AddTomlSerializer(this DependencyContainer container)
        {
            container.AddTomlSerializer(new());
        }

        public static void AddTomlSerializer(this DependencyContainer container, TomlSerializerOptions options)
        {
            container.Add(options);
            container.AddInterfacesAndSelf<TomlSerializer>();
        }
    }
}