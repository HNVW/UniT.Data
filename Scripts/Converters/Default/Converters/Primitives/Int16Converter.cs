#nullable enable
namespace UniT.Data.Converters.Default
{
    using System;
    using UnityEngine.Scripting;

    public sealed class Int16Converter : Converter<short>
    {
        private readonly IFormatProvider formatProvider;

        [Preserve]
        public Int16Converter(IFormatProvider formatProvider)
        {
            this.formatProvider = formatProvider;
        }

        protected override short ConvertFromString(string str)
        {
            return short.Parse(str, this.formatProvider);
        }

        protected override string ConvertToString(short obj)
        {
            return obj.ToString(this.formatProvider);
        }
    }
}