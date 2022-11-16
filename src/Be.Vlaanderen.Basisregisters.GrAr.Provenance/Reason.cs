namespace Be.Vlaanderen.Basisregisters.GrAr.Provenance
{
    using AggregateSource;

    public class Reason : StringValueObject<Reason>
    {
        public static readonly Reason CentralManagementCrab = new Reason("Centrale bijhouding CRAB");
        public static readonly Reason CentralManagementGrb = new Reason("Centrale bijhouding o.b.v. gebouwinformatie GRB");
        public static readonly Reason DecentralManagmentCrab = new Reason("Decentrale bijhouding CRAB");
        public static readonly Reason ManagementCrab = new Reason("Bijhouding op CRAB");
        public static readonly Reason CentralManagementBPost = new Reason("Centrale bijhouding o.b.v. bPost-bestand");

        public Reason(string reason)
            : base(reason)
        { }
    }
}
