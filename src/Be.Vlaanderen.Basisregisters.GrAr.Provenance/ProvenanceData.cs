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
        public Plan Plan { get; }

        public ProvenanceData(Provenance provenance)
        {
            Timestamp = provenance.Timestamp;
            Application = provenance.Application;
            Modification = provenance.Modification;
            Operator = provenance.Operator;
            Organisation = provenance.Organisation;
            Plan = provenance.Plan;
        }

        [JsonConstructor]
        private ProvenanceData(
            Instant timestamp,
            Application application,
            Modification modification,
            string @operator,
            Organisation organisation,
            Plan plan)
            : this(new Provenance(
                timestamp,
                application,
                plan,
                new Operator(@operator),
                modification,
                organisation))
        {
        }

        public Provenance ToProvenance() => new Provenance(
            Timestamp,
            Application,
            Plan,
            new Operator(Operator),
            Modification,
            Organisation);
    }
}
