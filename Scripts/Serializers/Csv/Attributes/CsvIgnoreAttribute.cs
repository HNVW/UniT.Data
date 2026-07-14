#nullable enable
namespace UniT.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Extensions;

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class CsvIgnoreAttribute : Attribute
    {
    }

    public static class CsvIgnoreAttributeExtensions
    {
        private static bool IsCsvIgnored(this FieldInfo field)
        {
            return field.GetCustomAttribute<CsvIgnoreAttribute>() is not null;
        }

        public static (List<FieldInfo> NormalFields, List<FieldInfo> NestedFields) GetCsvFields(this Type type)
        {
            return type.GetAllFields()
                .Where(static field => !field.IsCsvIgnored())
                .Split(static field => !typeof(ICsvData).IsAssignableFrom(field.FieldType));
        }
    }
}