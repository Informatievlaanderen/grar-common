namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.SnapshotProducer
{
    using Legacy;
    using Newtonsoft.Json;

    public class OsloResult
    {
        public OsloResult()
        { }

        public OsloIdentificator Identificator { get; set; }

        [JsonIgnore]
        public string JsonContent { get; set; }

        [JsonIgnore]
        public string? ETag { get; set; }
    }

    public class OsloIdentificator : Identificator
    {
        public OsloIdentificator()
            : base(string.Empty, string.Empty, string.Empty)
        { }
    }
}
