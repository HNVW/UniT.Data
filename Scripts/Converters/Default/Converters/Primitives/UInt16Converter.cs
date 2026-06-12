#nullable enable
namespace UniT.Data.Converters.Default
{
    using System;
    using UnityEngine.Scripting;

    public sealed class UInt16Converter : Converter<ushort>
    {
        private readonly IFormatProvider formatProvider;

        [Preserve]
        public UInt16Converter(IFormatProvider formatProvider)
        {
            this.formatProvider = formatProvider;
        }

        protected override ushort ConvertFromString(string str)
        {
            return ushort.Parse(str, this.formatProvider);
        }

        protected override string ConvertToString(ushort obj)
        {
            return obj.ToString(this.formatProvider);
        }
    }
}