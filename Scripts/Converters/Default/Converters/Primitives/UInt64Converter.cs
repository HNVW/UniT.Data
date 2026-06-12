#nullable enable
namespace UniT.Data.Converters.Default
{
    using System;
    using UnityEngine.Scripting;

    public sealed class UInt64Converter : Converter<ulong>
    {
        private readonly IFormatProvider formatProvider;

        [Preserve]
        public UInt64Converter(IFormatProvider formatProvider)
        {
            this.formatProvider = formatProvider;
        }

        protected override ulong ConvertFromString(string str)
        {
            return ulong.Parse(str, this.formatProvider);
        }

        protected override string ConvertToString(ulong obj)
        {
            return obj.ToString(this.formatProvider);
        }
    }
}