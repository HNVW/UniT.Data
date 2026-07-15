#nullable enable
namespace UniT.Data.DI
{
    using System;
    using System.Globalization;
    using InternalDI;
    using JsonConverter = JsonConverter;

    public static class ConverterManagerInternalDI
    {
        public static void AddConverterManager(this DependencyContainer container, bool addDefaultConverters = true)
        {
            if (addDefaultConverters)
            {
                container.AddDefaultConverters();
            }
            container.AddInterfaces<ConverterManager>();
        }

        public static void AddConverterManager(this DependencyContainer container, SeparatorConfig separatorConfig, IFormatProvider formatProvider)
        {
            container.AddDefaultConverters(separatorConfig, formatProvider);
            container.AddInterfaces<ConverterManager>();
        }

        public static void AddDefaultConverters(this DependencyContainer container)
        {
            container.AddDefaultConverters(
                separatorConfig: new(),
                formatProvider: CultureInfo.InvariantCulture
            );
        }

        public static void AddDefaultConverters(this DependencyContainer container, SeparatorConfig separatorConfig, IFormatProvider formatProvider)
        {
            container.Add(separatorConfig);
            container.Add(formatProvider);

            container.AddInterfaces<JsonConverter>();
            container.AddInterfaces<PrimitiveConverter>();
            container.AddInterfaces<DateTimeOffsetConverter>();
            container.AddInterfaces<TimeSpanConverter>();
            container.AddInterfaces<UriConverter>();
            container.AddInterfaces<GuidConverter>();
            container.AddInterfaces<EnumConverter>();
            container.AddInterfaces<NullableConverter>();

            container.AddInterfaces<TupleConverter>();
            container.AddInterfaces<AbstractTupleConverter>();
            container.AddInterfaces<ArrayConverter>();
            container.AddInterfaces<CollectionConverter>();         // Depends on ArrayConverter
            container.AddInterfaces<AbstractCollectionConverter>(); // Depends on ArrayConverter
            container.AddInterfaces<DictionaryConverter>();         // Depends on ArrayConverter
            container.AddInterfaces<ReadOnlyDictionaryConverter>(); // Depends on DictionaryConverter
            container.AddInterfaces<AbstractDictionaryConverter>(); // Depends on DictionaryConverter
        }
    }
}