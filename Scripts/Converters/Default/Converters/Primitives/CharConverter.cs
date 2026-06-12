#nullable enable
namespace UniT.Data.Converters.Default
{
    using UnityEngine.Scripting;

    public sealed class CharConverter : Converter<char>
    {
        [Preserve]
        public CharConverter()
        {
        }

        protected override char ConvertFromString(string str)
        {
            return char.Parse(str);
        }

        protected override string ConvertToString(char obj)
        {
            return obj.ToString();
        }
    }
}