#nullable enable
namespace UniT.Data.Converters
{
    using System;
    using UnityEngine.Scripting;

    public sealed class PrimitiveConverter : Converter
    {
        private readonly IFormatProvider formatProvider;

        [Preserve]
        public PrimitiveConverter(IFormatProvider formatProvider)
        {
            this.formatProvider = formatProvider;
        }

        protected override bool CanConvert(Type type) => typeof(IConvertible).IsAssignableFrom(type);

        protected override object ConvertFromString(Type type, string str)
        {
            return Convert.ChangeType(str, type, this.formatProvider);
        }

        protected override string ConvertToString(Type type, object obj)
        {
            return Convert.ToString(obj, this.formatProvider)!;
        }
    }
}