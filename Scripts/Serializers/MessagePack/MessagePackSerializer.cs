#nullable enable
namespace UniT.Data
{
    using System;
    using System.Reflection;
    using MessagePack;
    using UnityEngine.Scripting;
    using BaseSerializer = MessagePack.MessagePackSerializer;

    public sealed class MessagePackSerializer : Serializer
    {
        private readonly MessagePackSerializerOptions options;

        [Preserve]
        public MessagePackSerializer(MessagePackSerializerOptions options)
        {
            this.options = options;
        }

        protected override Type RawDataType => typeof(byte[]);

        protected override bool CanSerialize(Type type) => type.GetCustomAttribute<MessagePackObjectAttribute>() is not null;

        public override object Deserialize(Type type, object rawData)
        {
            return BaseSerializer.Deserialize(type, (byte[])rawData, this.options)!;
        }

        public override object Serialize(Type type, object data)
        {
            return BaseSerializer.Serialize(type, data, this.options);
        }

        public override T Deserialize<T>(object rawData)
        {
            return BaseSerializer.Deserialize<T>((byte[])rawData, this.options);
        }

        public override object Serialize<T>(T data)
        {
            return BaseSerializer.Serialize(data, this.options);
        }
    }
}