namespace Be.Vlaanderen.Basisregisters.GrAr.Provenance
{
    using System.Collections.Generic;

    public enum Plan
    {
        Unknown = 0,
        CentralManagementGrb = 1,
        CentralManagementCrab = 2,
        DecentralManagmentCrab = 3,
        ManagementCrab = 4,
        CentralManagementBPost = 5
    }

    public static class PlanExtensions
    {
        private static readonly IDictionary<Plan, string> PlanNames = new Dictionary<Plan, string>
        {
            {Plan.Unknown, ""},
            {Plan.CentralManagementCrab, "Centrale bijhouding CRAB"},
            {Plan.CentralManagementGrb, "Centrale bijhouding o.b.v. gebouwinformatie GRB"},
            {Plan.DecentralManagmentCrab, "Decentrale bijhouding CRAB"},
            {Plan.ManagementCrab, "Bijhouding op CRAB"},
            {Plan.CentralManagementBPost, "Centrale bijhouding bPost" }
        };

        public static string ToName(this Plan plan) => PlanNames[plan];
    }
}
