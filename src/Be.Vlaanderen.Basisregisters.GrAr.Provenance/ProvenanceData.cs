namespace Be.Vlaanderen.Basisregisters.GrAr.Provenance
{
    using Newtonsoft.Json;
    using NodaTime;

    public class ProvenanceData
    {
        public Instant Timestamp { get; }
        public Application Application { get; }
        public Modification Modification { get; }
        public string Operator { get; }
        public Organisation Organisation { get; }
        public string Reason { get; }

        public ProvenanceData(Provenance provenance)
        {
            Timestamp = provenance.Timestamp;
            Application = provenance.Application;
            Modification = provenance.Modification;
            Operator = provenance.Operator;
            Organisation = provenance.Organisation;
            Reason = provenance.Reason;
        }

        [JsonConstructor]
        private ProvenanceData(
            Instant timestamp,
            Application application,
            Modification modification,
            string @operator,
            Organisation organisation,
            string reason)
            : this(new Provenance(
                timestamp,
                application,
                new Reason(reason),
                new Operator(@operator),
                modification,
                organisation))
        {
        }

        public Provenance ToProvenance() => new Provenance(
            Timestamp,
            Application,
            new Reason(Reason),
            new Operator(Operator),
            Modification,
            Organisation);
    }
}
