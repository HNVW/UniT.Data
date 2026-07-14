#nullable enable
namespace UniT.Data.DI
{
    using System.Collections.Generic;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using VContainer;
    using JsonSerializer = JsonSerializer;

    public static class JsonSerializerVContainer
    {
        public static void RegisterJsonSerializer(this IContainerBuilder builder)
        {
            builder.RegisterJsonSerializer(new()
            {
                Culture = CultureInfo.InvariantCulture,
                TypeNameHandling = TypeNameHandling.Auto,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                ContractResolver = new WritablePropertyOnlyContractResolver(),
                Converters = new List<JsonConverter>
                {
                    new StringEnumConverter(),
                },
            });
        }

        public static void RegisterJsonSerializer(this IContainerBuilder builder, JsonSerializerSettings settings)
        {
            builder.RegisterInstance(settings);
            builder.Register<JsonSerializer>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}