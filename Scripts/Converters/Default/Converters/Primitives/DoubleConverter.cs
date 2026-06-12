#nullable enable
namespace UniT.Data.Converters.Default
{
    using System;
    using UnityEngine.Scripting;

    public sealed class DoubleConverter : Converter<double>
    {
        private readonly IFormatProvider formatProvider;

        [Preserve]
        public DoubleConverter(IFormatProvider formatProvider)
        {
            this.formatProvider = formatProvider;
        }

        protected override double ConvertFromString(string str)
        {
            return double.Parse(str, this.formatProvider);
        }

        protected override string ConvertToString(double obj)
        {
            return obj.ToString(this.formatProvider);
        }
    }
}