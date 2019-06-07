namespace Be.Vlaanderen.Basisregisters.GrAr.Provenance
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Crab;
    using NodaTime;
    using NodaHelpers = Common.NodaHelpers;

    public class CrabProvenanceFactory
    {
        private static readonly LocalDate VlmEndDate = new LocalDate(2006, 04, 01);
        private static readonly LocalDate AgivEndDate = new LocalDate(2016, 01, 01);

        private static readonly Regex CrabWstEditServiceRegex = new Regex(".*:.*/agiv-services.*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex LaraRegex = new Regex(".*:.*", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Dictionary<CrabOrganisation, Organisation> MapCrabOrganisations = new Dictionary<CrabOrganisation, Organisation>
        {
            {CrabOrganisation.Akred, Organisation.Akred},
            {CrabOrganisation.Other, Organisation.Other},
            {CrabOrganisation.DePost, Organisation.DePost},
            {CrabOrganisation.Municipality, Organisation.Municipality},
            {CrabOrganisation.NavTeq, Organisation.NavTeq},
            {CrabOrganisation.Ngi, Organisation.Ngi},
            {CrabOrganisation.NationalRegister, Organisation.NationalRegister},
            {CrabOrganisation.TeleAtlas, Organisation.TeleAtlas},
            {CrabOrganisation.Vkbo, Organisation.Vkbo}
        };

        private static Operator MapOperator(CrabOperator crabOperator)
        {
            if (crabOperator == null)
                return null;

            var trimmed = ((string)crabOperator).Trim();
            if (trimmed.Equals(@"VLM\CRABSSISservice", StringComparison.OrdinalIgnoreCase) ||
                trimmed.Equals(@"VLM\CRABSSisServiceBeta", StringComparison.OrdinalIgnoreCase) ||
                trimmed.Equals(@"VLM\GRBCrabMatchingProd", StringComparison.OrdinalIgnoreCase) ||
                trimmed.Equals(@"VLM\GRBCrabMatchingBeta", StringComparison.OrdinalIgnoreCase))
                return new Operator("ServiceAccount");

            return new Operator(crabOperator);
        }

        private static Modification MapModification(
            CrabModification? crabModification,
            int version,
            bool isRemoved)
        {
            if (crabModification == null)
                return Modification.Unknown;

            if (version == 1)
                return Modification.Insert;

            if (isRemoved)
                return Modification.Delete;

            return Modification.Update;
        }

        private static Organisation MapOrganisation(CrabOrganisation? crabOrganisation, Instant timestamp)
        {
            if (crabOrganisation == null)
                return Organisation.Unknown;

            if (crabOrganisation == CrabOrganisation.Vlm)
            {
                if (timestamp < VlmEndDate.AtStartOfDayInZone(NodaHelpers.BelgianDateTimeZone).ToInstant())
                    return Organisation.Vlm;

                if (timestamp < AgivEndDate.AtStartOfDayInZone(NodaHelpers.BelgianDateTimeZone).ToInstant())
                    return Organisation.Agiv;

                return Organisation.Aiv;
            }

            return MapCrabOrganisations[crabOrganisation.Value];
        }

        private static Reason MapReason(CrabOperator crabOperator)
        {
            if (string.IsNullOrWhiteSpace(crabOperator))
                return null;

            string @operator = crabOperator;

            if (@operator.Equals("VLM\\GRBCrabMatchingProd", StringComparison.OrdinalIgnoreCase) ||
                @operator.Equals("VLM\\GRBCrabMatchingBeta", StringComparison.OrdinalIgnoreCase))
                return Reason.CentralManagementGrb;

            if (@operator.StartsWith("VLM", StringComparison.OrdinalIgnoreCase))
                return Reason.CentralManagementCrab;

            if (CrabWstEditServiceRegex.IsMatch(@operator) || LaraRegex.IsMatch(@operator))
                return Reason.DecentralManagmentCrab;

            return Reason.ManagementCrab;
        }

        private static Application MapApplication(CrabOperator crabOperator)
        {
            if (string.IsNullOrWhiteSpace(crabOperator))
                return Application.Unknown;

            string @operator = crabOperator;

            if (@operator.Equals("VLM\\CRABSSISservice", StringComparison.OrdinalIgnoreCase))
                return Application.CrabSsisService;

            if (@operator.Equals("VLM\\CRABSSisServiceBeta", StringComparison.OrdinalIgnoreCase))
                return Application.CrabSsisService;

            if (@operator.Equals("VLM\\GRBCrabMatchingProd", StringComparison.OrdinalIgnoreCase) ||
                @operator.Equals("VLM\\GRBCrabMatchingBeta", StringComparison.OrdinalIgnoreCase))
                return Application.GrbCrabMatching;

            if (CrabWstEditServiceRegex.IsMatch(@operator))
                return Application.CrabWstEditService;

            if (LaraRegex.IsMatch(@operator))
                return Application.Lara;

            return Application.Unknown;
        }

        public Provenance CreateFrom(
            int version,
            bool isRemoved,
            CrabTimestamp timestamp,
            CrabModification? crabModification,
            CrabOperator crabOperator,
            CrabOrganisation? crabOrganisation) => new Provenance(
            (Instant) timestamp,
            MapApplication(crabOperator),
            MapReason(crabOperator),
            MapOperator(crabOperator),
            MapModification(crabModification, version, isRemoved),
            MapOrganisation(crabOrganisation, timestamp));

        public Provenance CreateFrom(
            Modification modification,
            CrabTimestamp timestamp,
            CrabModification? crabModification,
            CrabOperator @operator,
            CrabOrganisation? organisation)
        {
            return CreateFrom(
                modification == Modification.Insert ? 1 : 2,
                modification == Modification.Delete,
                timestamp,
                crabModification,
                @operator,
                organisation);
        }
    }
}
