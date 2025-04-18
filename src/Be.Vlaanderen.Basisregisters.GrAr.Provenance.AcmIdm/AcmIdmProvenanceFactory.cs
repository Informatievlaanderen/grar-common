﻿namespace Be.Vlaanderen.Basisregisters.GrAr.Provenance.AcmIdm
{
    using System;
    using System.Security.Claims;
    using Basisregisters.Auth.AcmIdm;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using NodaTime;

    public class AcmIdmProvenanceFactory : IProvenanceFactory
    {
        public const string OvoCodeDigitaalVlaanderen = "OVO002949";

        private readonly Application _application;
        private readonly IActionContextAccessor _actionContextAccessor;

        public AcmIdmProvenanceFactory(
            Application application,
            IActionContextAccessor actionContextAccessor)
        {
            _application = application;
            _actionContextAccessor = actionContextAccessor;
        }

        public Provenance Create(Reason reason, Modification modification)
        {
            if (_actionContextAccessor.ActionContext is null)
            {
                throw new NullReferenceException("No ActionContext on the ActionContextAccessor");
            }

            var contextUser = _actionContextAccessor.ActionContext.HttpContext.User;
            var ovoCode = _actionContextAccessor.ActionContext.HttpContext.FindOvoCodeClaim();
            var organisation = Organisation.Other;

            if (ovoCode == OvoCodeDigitaalVlaanderen)
            {
                organisation = Organisation.DigitaalVlaanderen;
            }
            else if (!string.IsNullOrEmpty(contextUser.FindFirstValue(AcmIdmClaimTypes.NisCode)))
            {
                organisation = Organisation.Municipality;
            }

            return new Provenance(
                SystemClock.Instance.GetCurrentInstant(),
                _application,
                reason,
                new Operator(ovoCode ?? contextUser.FindFirstValue(AcmIdmClaimTypes.VoOrgCode) ?? string.Empty),
                modification,
                organisation);
        }
    }
}
