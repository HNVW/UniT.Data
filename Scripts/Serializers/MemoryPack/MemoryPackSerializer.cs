#nullable enable
namespace UniT.Data
{
    using System;
    using System.Reflection;
    using MemoryPack;
    using UnityEngine.Scripting;
    using BaseSerializer = MemoryPack.MemoryPackSerializer;

    public sealed class MemoryPackSerializer : Serializer<byte[], object>
    {
        private readonly MemoryPackSerializerOptions options;

        [Preserve]
        public MemoryPackSerializer(MemoryPackSerializerOptions options)
        {
            this.options = options;
        }

        protected override bool CanSerialize(Type type) => type.GetCustomAttribute<MemoryPackableAttribute>() is not null;

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
            return BaseSerializer.Deserialize<T>(rawData, this.options)!;
        }

        public override byte[] Serialize<T>(T data)
        {
            return BaseSerializer.Serialize(data, this.options);
        }
    }
}