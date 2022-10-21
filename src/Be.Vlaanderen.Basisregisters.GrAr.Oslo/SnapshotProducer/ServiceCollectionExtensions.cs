namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.SnapshotProducer
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static void AddPublicApiHttpProxy(this IServiceCollection services, string osloApiUrl)
        {
            if (string.IsNullOrEmpty(osloApiUrl))
            {
                throw new ArgumentNullException(nameof(osloApiUrl),"OsloApiUrl config property not set.");
            }

            services.AddHttpClient<IOsloProxy, OsloProxy>(c =>
            {
                c.BaseAddress = new Uri(osloApiUrl.TrimEnd('/'));
            });
        }
    }
}
