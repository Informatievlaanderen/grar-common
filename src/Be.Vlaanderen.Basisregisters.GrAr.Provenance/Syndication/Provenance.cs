namespace Be.Vlaanderen.Basisregisters.GrAr.Provenance.Syndication
{
    using System.Runtime.Serialization;
    using GrAr.Provenance;

    [DataContract(Name = "Creatie", Namespace = "")]
    public class Provenance
    {
        [DataMember(Name = "Organisatie", Order = 0)]
        public string Organisation { get; set; }

        [DataMember(Name = "Reden", Order = 1)]
        public string Reason { get; set; }

        public Provenance(
            Organisation? organisation,
            Reason reason)
        {
            Organisation = organisation?.ToName();
            Reason = reason;
        }
    }
}
