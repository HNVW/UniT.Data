#nullable enable
namespace UniT.Data.Converters
{
    using System;
    using UnityEngine.Scripting;

    public sealed class DateTimeOffsetConverter : Converter<DateTimeOffset>
    {
        private readonly IFormatProvider formatProvider;

        [Preserve]
        public DateTimeOffsetConverter(IFormatProvider formatProvider)
        {
            this.formatProvider = formatProvider;
        }

        protected override DateTimeOffset ConvertFromString(string str)
        {
            return DateTimeOffset.Parse(str, this.formatProvider);
        }

        protected override string ConvertToString(DateTimeOffset obj)
        {
            return obj.ToString(this.formatProvider);
        }
    }
}