#nullable enable
namespace UniT.Data
{
    using System;
    using Google.Protobuf;
    using UnityEngine.Scripting;

    public sealed class ProtobufSerializer : Serializer
    {
        [Preserve]
        public ProtobufSerializer()
        {
        }

        protected override Type RawDataType => typeof(byte[]);

        protected override bool CanSerialize(Type type) => typeof(IMessage).IsAssignableFrom(type);

        public override object Deserialize(Type type, object rawData)
        {
            var data = (IMessage)Activator.CreateInstance(type);
            data.MergeFrom((byte[])rawData);
            return data;
        }

        public override object Serialize(Type type, object data)
        {
            return ((IMessage)data).ToByteArray();
        }
    }
}