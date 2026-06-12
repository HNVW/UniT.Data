#nullable enable
namespace UniT.Data.Converters.Default
{
    using UnityEngine.Scripting;

    public sealed class StringConverter : Converter<string>
    {
        [Preserve]
        public StringConverter()
        {
        }

        protected override string ConvertFromString(string str)
        {
            return str;
        }

        protected override string ConvertToString(string obj)
        {
            return obj;
        }
    }
}