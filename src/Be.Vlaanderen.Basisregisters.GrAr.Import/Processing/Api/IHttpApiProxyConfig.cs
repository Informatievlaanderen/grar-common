namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.Api
{
    using System;

    public interface IHttpApiProxyConfig
    {
        Uri BaseUrl { get; }
        string ImportEndpoint { get; }
        string ImportBatchStatusEndpoint { get; }
        int HttpTimeoutMinutes { get; }

        /// <summary>
        /// If empty, no basic auth will be used
        /// </summary>
        string AuthUserName { get; }

        /// <summary>
        /// If empty, no basic auth will be used
        /// </summary>
        string AuthPassword { get; }
    }
}
