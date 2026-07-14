#nullable enable
namespace UniT.Data.DI
{
    using System.Collections.Generic;
    using System.Globalization;
    using InternalDI;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using JsonSerializer = JsonSerializer;

    public static class JsonSerializerInternalDI
    {
        public static void AddJsonSerializer(this DependencyContainer container)
        {
            container.AddJsonSerializer(new()
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

        public static void AddJsonSerializer(this DependencyContainer container, JsonSerializerSettings settings)
        {
            container.Add(settings);
            container.AddInterfacesAndSelf<JsonSerializer>();
        }
    }
}