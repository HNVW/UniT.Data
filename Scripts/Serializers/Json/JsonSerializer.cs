#nullable enable
namespace UniT.Data
{
    using System;
    using Newtonsoft.Json;
    using UnityEngine.Scripting;

    public sealed class JsonSerializer : Serializer<string, object>
    {
        private readonly JsonSerializerSettings settings;

        [Preserve]
        public JsonSerializer(JsonSerializerSettings settings)
        {
            this.settings = settings;
        }

        protected override bool CanSerialize(Type type) => true;

        public override object Deserialize(Type type, string rawData)
        {
            return JsonConvert.DeserializeObject(rawData, type, this.settings)!;
        }

        public override string Serialize(Type type, object data)
        {
            return JsonConvert.SerializeObject(data, type, this.settings);
        }

        public override T Deserialize<T>(string rawData)
        {
            return JsonConvert.DeserializeObject<T>(rawData, this.settings)!;
        }

        public override string Serialize<T>(T data)
        {
            return JsonConvert.SerializeObject(data, this.settings);
        }
    }
}