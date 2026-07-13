#nullable enable
namespace UniT.Data.Serializers.DI
{
    using System.Globalization;
    using CsvHelper.Configuration;
    using InternalDI;

    public static class CsvSerializerInternalDI
    {
        public static void AddCsvSerializer(this DependencyContainer container)
        {
            container.AddCsvSerializer(new(CultureInfo.InvariantCulture)
            {
                MissingFieldFound = null,
                PrepareHeaderForMatch = static args => args.Header.ToLowerInvariant(),
            });
        }

        public static void AddCsvSerializer(this DependencyContainer container, CsvConfiguration configuration)
        {
            container.Add(configuration);
            container.AddInterfacesAndSelf<CsvSerializer>();
        }
    }
}