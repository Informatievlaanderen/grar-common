namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.Json
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using NodaTime;
    using NodaTime.Serialization.JsonNet;

    public static class JsonSerializerSettingsExtensions
    {
        /// <summary>
        ///     Sets up and adds additional converters for Importers to the JsonSerializerSettings
        /// </summary>
        /// <param name="source"></param>
        /// <returns>the updated JsonSerializerSettings</returns>
        public static JsonSerializerSettings ConfigureForCrabImports(this JsonSerializerSettings source)
        {
            source.ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy() //TODO is this necessary?
            };

            source.MissingMemberHandling = MissingMemberHandling.Ignore;
            source.MaxDepth = 32;
            source.TypeNameHandling = TypeNameHandling.None;
            source.DateFormatHandling = DateFormatHandling.IsoDateFormat;

            return source
                .ConfigureForNodaTime(DateTimeZoneProviders.Tzdb)
                .WithIsoIntervalConverter();
        }
    }
}
