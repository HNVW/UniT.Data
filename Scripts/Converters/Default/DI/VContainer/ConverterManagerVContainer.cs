#nullable enable
namespace UniT.Data.Converters.DI
{
    using System;
    using System.Globalization;
    using VContainer;
    using JsonConverter = JsonConverter;

    public static class ConverterManagerVContainer
    {
        public static void RegisterConverterManager(this IContainerBuilder builder, bool registerDefaultConverters = true)
        {
            if (registerDefaultConverters)
            {
                builder.RegisterDefaultConverters();
            }
            builder.Register<ConverterManager>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        public static void RegisterDefaultConverters(this IContainerBuilder builder)
        {
            builder.RegisterDefaultConverters(
                separatorConfig: new(),
                formatProvider: CultureInfo.InvariantCulture
            );
        }

        public static void RegisterDefaultConverters(this IContainerBuilder builder, SeparatorConfig separatorConfig, IFormatProvider formatProvider)
        {
            builder.RegisterInstance(separatorConfig);
            builder.RegisterInstance(formatProvider);

            builder.Register<JsonConverter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PrimitiveConverter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<DateTimeOffsetConverter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TimeSpanConverter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<UriConverter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<GuidConverter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EnumConverter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<NullableConverter>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<TupleConverter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<AbstractTupleConverter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ArrayConverter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<CollectionConverter>(Lifetime.Singleton).AsImplementedInterfaces();         // Depends on ArrayConverter
            builder.Register<AbstractCollectionConverter>(Lifetime.Singleton).AsImplementedInterfaces(); // Depends on ArrayConverter
            builder.Register<DictionaryConverter>(Lifetime.Singleton).AsImplementedInterfaces();         // Depends on ArrayConverter
            builder.Register<ReadOnlyDictionaryConverter>(Lifetime.Singleton).AsImplementedInterfaces(); // Depends on DictionaryConverter
            builder.Register<AbstractDictionaryConverter>(Lifetime.Singleton).AsImplementedInterfaces(); // Depends on DictionaryConverter
        }
    }
}