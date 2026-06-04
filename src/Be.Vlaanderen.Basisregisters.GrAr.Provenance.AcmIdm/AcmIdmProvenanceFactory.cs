namespace Be.Vlaanderen.Basisregisters.GrAr.Provenance.AcmIdm
{
    using System;
    using System.Security.Claims;
    using Basisregisters.Auth.AcmIdm;
    using Microsoft.AspNetCore.Http;
    using NodaTime;

    public class AcmIdmProvenanceFactory : IProvenanceFactory
    {
        public const string OvoCodeDigitaalVlaanderen = "OVO002949";

        private readonly Application _application;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AcmIdmProvenanceFactory(
            Application application,
            IHttpContextAccessor httpContextAccessor)
        {
            _application = application;
            _httpContextAccessor = httpContextAccessor;
        }

        public Provenance Create(Reason reason, Modification modification)
        {
            if (_httpContextAccessor.HttpContext is null)
            {
                throw new NullReferenceException("No ActionContext on the ActionContextAccessor");
            }

            var contextUser = _httpContextAccessor.HttpContext.User;
            var ovoCode = _httpContextAccessor.HttpContext.FindOvoCodeClaim();
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
