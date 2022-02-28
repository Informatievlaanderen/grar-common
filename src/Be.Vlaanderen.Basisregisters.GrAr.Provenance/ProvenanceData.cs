namespace Be.Vlaanderen.Basisregisters.GrAr.Provenance
{
    using System.Collections.Generic;
    using Common;
    using Newtonsoft.Json;
    using NodaTime;

    public class ProvenanceData : IHaveHashFields
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

        public IEnumerable<string> GetHashFields()
        {
            yield return Timestamp.ToString();
            yield return Application.ToString();
            yield return Modification.ToString();
            yield return Operator;
            yield return Organisation.ToString();
            yield return Reason;
        }
    }
}
