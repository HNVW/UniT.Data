#nullable enable
namespace UniT.Data.Converters.Default
{
    using System;
    using UnityEngine.Scripting;

    public sealed class TimeSpanConverter : Converter<TimeSpan>
    {
        [Preserve]
        public TimeSpanConverter()
        {
        }

        protected override TimeSpan ConvertFromString(string str)
        {
            return TimeSpan.Parse(str);
        }

        protected override string ConvertToString(TimeSpan obj)
        {
            return obj.ToString();
        }
    }
}