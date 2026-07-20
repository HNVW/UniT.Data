#nullable enable
namespace UniT.Data
{
    using System;
    using UnityEngine.Scripting;

    public sealed class DefaultSerializer : Serializer
    {
        private readonly IConverterManager converterManager;

        [Preserve]
        public DefaultSerializer(IConverterManager converterManager)
        {
            this.converterManager = converterManager;
        }

        protected override Type RawDataType => typeof(string);

        protected override bool CanSerialize(Type type) => true;

        public override object Deserialize(Type type, object rawData)
        {
            return this.converterManager.ConvertFromString(type, (string)rawData);
        }

        public override object Serialize(Type type, object data)
        {
            return this.converterManager.ConvertToString(type, data);
        }
    }
}