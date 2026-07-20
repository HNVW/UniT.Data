#nullable enable
namespace UniT.Data
{
    using System;
    using Tomlyn;
    using UnityEngine.Scripting;
    using BaseSerializer = Tomlyn.TomlSerializer;

    public sealed class TomlSerializer : Serializer
    {
        private readonly TomlSerializerOptions options;

        [Preserve]
        public TomlSerializer(TomlSerializerOptions options)
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