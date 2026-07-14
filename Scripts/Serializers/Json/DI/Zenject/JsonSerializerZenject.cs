#nullable enable
namespace UniT.Data.DI
{
    using System.Collections.Generic;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Zenject;
    using JsonSerializer = JsonSerializer;

    public static class JsonSerializerZenject
    {
        public static void BindJsonSerializer(this DiContainer container)
        {
            container.BindJsonSerializer(new()
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

        public static void BindJsonSerializer(this DiContainer container, JsonSerializerSettings settings)
        {
            container.BindInstance(settings);
            container.BindInterfacesAndSelfTo<JsonSerializer>().AsSingle();
        }
    }
}