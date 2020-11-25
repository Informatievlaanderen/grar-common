namespace Be.Vlaanderen.Basisregisters.Oslo.ContextProperties.Generator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;

    class Program
    {
        private static readonly IConfiguration Configuration;
        private static readonly ContextPropertiesDirectory ContextPropertiesDirectory;

        static Program()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddJsonFile($"appsettings.{Environment.MachineName.ToLowerInvariant()}.json", optional: true, reloadOnChange: false)
                .Build();

            ContextPropertiesDirectory = new ContextPropertiesDirectory(Configuration);
        }

        static async Task Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, cancelArgs) => cancellationTokenSource.Cancel();

            var directory = ContextPropertiesDirectory.CreateVersionDirectory();
            foreach (var contextInfo in GetContexts())
                await CreateContextPropertiesFile(directory, contextInfo, cancellationTokenSource.Token);
        }

        private static async Task CreateContextPropertiesFile(
            DirectoryInfo directory,
            ContextInformation contextInfo,
            CancellationToken cancellationToken)
        {
            var context = await new ContextSource(contextInfo.SourceUrl).Fetch(cancellationToken);

            var fileName = Path.Combine(directory.FullName, contextInfo.FileName + ".json"); // dump json for now
            await File.WriteAllTextAsync(fileName, context.ToString(), cancellationToken);

            // ...maybe there needs to happen something here too

            // write cs file
            // profit!
        }

        private static IEnumerable<ContextInformation> GetContexts()
            => Configuration
                .GetSection("jsonld-context-urls")
                .GetChildren()
                .Select(x => new ContextInformation(x.Key, new Uri(x.Value)));
    }
}
