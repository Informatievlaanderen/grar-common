namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.CrabImport
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCrabImportMigrations(this IApplicationBuilder builder)
        {
            var services = builder.ApplicationServices;

            CrabImportMigrationsHelper.Run(
                services.GetRequiredService<Configuration>(),
                services.GetRequiredService<MigrationsSchema>(),
                services.GetRequiredService<CrabImportSchema>(),
                services.GetRequiredService<ILoggerFactory>());

            return builder;
        }
    }
}
