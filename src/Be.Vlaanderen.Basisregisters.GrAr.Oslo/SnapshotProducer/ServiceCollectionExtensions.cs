namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.SnapshotProducer
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static void AddPublicApiHttpProxy(this IServiceCollection services, string publicApiUrl)
        {
            if (string.IsNullOrEmpty(publicApiUrl))
            {
                throw new ArgumentNullException(nameof(publicApiUrl),"PublicApiUrl config property not set.");
            }

            services.AddHttpClient<IPublicApiHttpProxy, PublicApiHttpProxy>(c =>
            {
                c.BaseAddress = new Uri(publicApiUrl.TrimEnd('/'));
            });
        }
    }
}
