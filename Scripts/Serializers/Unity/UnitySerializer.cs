#nullable enable
namespace UniT.Data
{
    using System;
    using UnityEngine.Scripting;
    using Object = UnityEngine.Object;

    public sealed class UnitySerializer : Serializer
    {
        [Preserve]
        public UnitySerializer()
        {
        }

        protected override Type RawDataType => typeof(Object);

        protected override bool CanSerialize(Type type) => typeof(Object).IsAssignableFrom(type);

        public override object Deserialize(Type type, object rawData) => rawData;

        public override object Serialize(Type type, object data) => data;
    }
}