#nullable enable
namespace UniT.Data.Serializers
{
    using System;
    using System.Reflection;
    using MessagePack;
    using UnityEngine.Scripting;
    using BaseSerializer = MessagePack.MessagePackSerializer;

    public sealed class MessagePackSerializer : Serializer<byte[], object>
    {
        private readonly MessagePackSerializerOptions options;

        [Preserve]
        public MessagePackSerializer(MessagePackSerializerOptions options)
        {
            this.options = options;
        }

        protected override bool CanSerialize(Type type) => base.CanSerialize(type) && type.GetCustomAttribute<MessagePackObjectAttribute>() is not null;

        public override object Deserialize(Type type, byte[] rawData)
        {
            return BaseSerializer.Deserialize(type, rawData, this.options)!;
        }

        public override byte[] Serialize(Type type, object data)
        {
            return BaseSerializer.Serialize(type, data, this.options);
        }

        public override T Deserialize<T>(byte[] rawData)
        {
            return BaseSerializer.Deserialize<T>(rawData, this.options);
        }

        public override byte[] Serialize<T>(T data)
        {
            return BaseSerializer.Serialize(data, this.options);
        }
    }
}