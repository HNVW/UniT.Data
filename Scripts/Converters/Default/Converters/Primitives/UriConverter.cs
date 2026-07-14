#nullable enable
namespace UniT.Data
{
    using System;
    using UnityEngine.Scripting;

    public sealed class UriConverter : Converter<Uri>
    {
        [Preserve]
        public UriConverter()
        {
        }

        protected override Uri ConvertFromString(string str)
        {
            return new(str);
        }

        protected override string ConvertToString(Uri obj)
        {
            return obj.ToString();
        }
    }
}