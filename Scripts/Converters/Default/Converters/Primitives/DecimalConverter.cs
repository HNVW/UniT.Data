#nullable enable
namespace UniT.Data.Converters.Default
{
    using System;
    using UnityEngine.Scripting;

    public sealed class DecimalConverter : Converter<decimal>
    {
        private readonly IFormatProvider formatProvider;

        [Preserve]
        public DecimalConverter(IFormatProvider formatProvider)
        {
            this.formatProvider = formatProvider;
        }

        protected override decimal ConvertFromString(string str)
        {
            return decimal.Parse(str, this.formatProvider);
        }

        protected override string ConvertToString(decimal obj)
        {
            return obj.ToString(this.formatProvider);
        }
    }
}