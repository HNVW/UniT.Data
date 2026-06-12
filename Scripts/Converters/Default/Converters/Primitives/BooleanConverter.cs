#nullable enable
namespace UniT.Data.Converters.Default
{
    using UnityEngine.Scripting;

    public sealed class BooleanConverter : Converter<bool>
    {
        [Preserve]
        public BooleanConverter()
        {
        }

        protected override bool ConvertFromString(string str)
        {
            return bool.Parse(str);
        }

        protected override string ConvertToString(bool obj)
        {
            return obj.ToString();
        }
    }
}