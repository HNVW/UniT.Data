#nullable enable
namespace UniT.Data.Converters.Default
{
    using System;
    using UnityEngine.Scripting;

    public sealed class ByteConverter : Converter<byte>
    {
        private readonly IFormatProvider formatProvider;

        [Preserve]
        public ByteConverter(IFormatProvider formatProvider)
        {
            this.formatProvider = formatProvider;
        }

        protected override byte ConvertFromString(string str)
        {
            return byte.Parse(str, this.formatProvider);
        }

        protected override string ConvertToString(byte obj)
        {
            return obj.ToString(this.formatProvider);
        }
    }
}