#nullable enable
namespace UniT.Data.DI
{
    using System;
    using System.Globalization;
    using Zenject;
    using JsonConverter = JsonConverter;

    public static class ConverterManagerZenject
    {
        public static void BindConverterManager(this DiContainer container, bool bindDefaultConverters = true)
        {
            if (bindDefaultConverters)
            {
                container.BindDefaultConverters();
            }
            container.BindInterfacesTo<ConverterManager>().AsSingle();
        }

        public static void BindDefaultConverters(this DiContainer container)
        {
            container.BindDefaultConverters(
                separatorConfig: new(),
                formatProvider: CultureInfo.InvariantCulture
            );
        }

        public static void BindDefaultConverters(this DiContainer container, SeparatorConfig separatorConfig, IFormatProvider formatProvider)
        {
            container.BindInstance(separatorConfig);
            container.BindInstance(formatProvider);

            container.BindInterfacesTo<JsonConverter>().AsSingle();
            container.BindInterfacesTo<PrimitiveConverter>().AsSingle();
            container.BindInterfacesTo<DateTimeOffsetConverter>().AsSingle();
            container.BindInterfacesTo<TimeSpanConverter>().AsSingle();
            container.BindInterfacesTo<UriConverter>().AsSingle();
            container.BindInterfacesTo<GuidConverter>().AsSingle();
            container.BindInterfacesTo<EnumConverter>().AsSingle();
            container.BindInterfacesTo<NullableConverter>().AsSingle();

            container.BindInterfacesTo<TupleConverter>().AsSingle();
            container.BindInterfacesTo<AbstractTupleConverter>().AsSingle();
            container.BindInterfacesTo<ArrayConverter>().AsSingle();
            container.BindInterfacesTo<CollectionConverter>().AsSingle();         // Depends on ArrayConverter
            container.BindInterfacesTo<AbstractCollectionConverter>().AsSingle(); // Depends on ArrayConverter
            container.BindInterfacesTo<DictionaryConverter>().AsSingle();         // Depends on ArrayConverter
            container.BindInterfacesTo<ReadOnlyDictionaryConverter>().AsSingle(); // Depends on DictionaryConverter
            container.BindInterfacesTo<AbstractDictionaryConverter>().AsSingle(); // Depends on DictionaryConverter
        }
    }
}