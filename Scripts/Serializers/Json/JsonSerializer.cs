#nullable enable
namespace UniT.Data
{
    using System;
    using Newtonsoft.Json;
    using UnityEngine.Scripting;

    public sealed class JsonSerializer : Serializer
    {
        private readonly JsonSerializerSettings settings;

        [Preserve]
        public JsonSerializer(JsonSerializerSettings settings)
        {
            this.settings = settings;
        }

        protected override Type RawDataType => typeof(string);

        protected override bool CanSerialize(Type type) => true;

        public override object Deserialize(Type type, object rawData)
        {
            return JsonConvert.DeserializeObject((string)rawData, type, this.settings)!;
        }

        public override object Serialize(Type type, object data)
        {
            return JsonConvert.SerializeObject(data, type, this.settings);
        }

        public override T Deserialize<T>(object rawData)
        {
            return JsonConvert.DeserializeObject<T>((string)rawData, this.settings)!;
        }

        public override object Serialize<T>(T data)
        {
            return JsonConvert.SerializeObject(data, this.settings);
        }
    }
}