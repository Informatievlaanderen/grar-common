namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing
{
    using Newtonsoft.Json;

    public class ImportFeed
    {
        public string Name { get; set; }

        [JsonIgnore]
        public string Id => Name?.ToLowerInvariant().Trim() ?? string.Empty;
    }
}
