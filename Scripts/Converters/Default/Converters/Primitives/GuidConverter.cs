#nullable enable
namespace UniT.Data.Converters.Default
{
    using System;
    using UnityEngine.Scripting;

    public sealed class GuidConverter : Converter<Guid>
    {
        [Preserve]
        public GuidConverter()
        {
        }

        protected override Guid ConvertFromString(string str)
        {
            return Guid.Parse(str);
        }

        protected override string ConvertToString(Guid obj)
        {
            return obj.ToString();
        }
    }
}