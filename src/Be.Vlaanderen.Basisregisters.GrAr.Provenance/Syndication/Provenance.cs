namespace Be.Vlaanderen.Basisregisters.GrAr.Provenance.Syndication
{
    using System.Runtime.Serialization;
    using GrAr.Provenance;

    [DataContract(Name = "Creatie", Namespace = "")]
    public class Provenance
    {
        [DataMember(Name = "Organisatie", Order = 0)]
        public string Organisation { get; set; }

        [DataMember(Name = "Plan", Order = 1)]
        public string Plan { get; set; }

        public Provenance(
            Organisation? organisation,
            Plan? plan)
        {
            Organisation = organisation?.ToName();
            Plan = plan?.ToName();
        }
    }
}
