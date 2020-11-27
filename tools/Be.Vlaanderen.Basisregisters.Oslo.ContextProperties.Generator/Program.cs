namespace Be.Vlaanderen.Basisregisters.Oslo.ContextProperties.Generator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;

    public class Program
    {
        private static readonly IConfiguration Configuration;

        static Program()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddJsonFile($"appsettings.{Environment.MachineName.ToLowerInvariant()}.json", optional: true, reloadOnChange: false)
                .Build();
        }

        static async Task Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, cancelArgs) => cancellationTokenSource.Cancel();

            // suggest version directory, allow user to override suggestion
            // console read, not in config
            var contextPropertiesFileBuilder = new ContextPropertiesFileBuilder(Configuration);

            foreach (var contextInfo in GetContextInfos())
                await contextPropertiesFileBuilder.CreateContentPropertiesFile(contextInfo, cancellationTokenSource.Token);
        }

        private static IEnumerable<ContextInformation> GetContextInfos()
            => Configuration
                .GetSection("jsonld-context-urls")
                .GetChildren()
                .Select(x => new ContextInformation(x.Key, new Uri(x.Value)));
    }
}
