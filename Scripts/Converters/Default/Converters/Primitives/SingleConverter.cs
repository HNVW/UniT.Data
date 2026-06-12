#nullable enable
namespace UniT.Data.Converters.Default
{
    using System;
    using UnityEngine.Scripting;

    public sealed class SingleConverter : Converter<float>
    {
        private readonly IFormatProvider formatProvider;

        [Preserve]
        public SingleConverter(IFormatProvider formatProvider)
        {
            this.formatProvider = formatProvider;
        }

        protected override float ConvertFromString(string str)
        {
            return float.Parse(str, this.formatProvider);
        }

        protected override string ConvertToString(float obj)
        {
            return obj.ToString(this.formatProvider);
        }
    }
}