#nullable enable
namespace UniT.Data
{
    using System;
    using SharpYaml;
    using UnityEngine.Scripting;
    using BaseSerializer = SharpYaml.YamlSerializer;

    public sealed class YamlSerializer : Serializer
    {
        private readonly YamlSerializerOptions options;

        [Preserve]
        public YamlSerializer(YamlSerializerOptions options)
        {
            this.options = options;
        }

        protected override Type RawDataType => typeof(string);

        protected override bool CanSerialize(Type type) => true;

        public override object Deserialize(Type type, object rawData)
        {
            return BaseSerializer.Deserialize((string)rawData, type, this.options)!;
        }

        public override object Serialize(Type type, object data)
        {
            return BaseSerializer.Serialize(data, type, this.options);
        }

        public override T Deserialize<T>(object rawData)
        {
            return BaseSerializer.Deserialize<T>((string)rawData, this.options)!;
        }

        public override object Serialize<T>(T data)
        {
            return BaseSerializer.Serialize(data, this.options);
        }
    }
}