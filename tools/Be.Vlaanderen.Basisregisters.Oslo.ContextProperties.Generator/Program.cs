namespace Be.Vlaanderen.Basisregisters.Oslo.ContextProperties.Generator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
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
            var directory = ContextPropertiesDirectory.CreateVersionDirectory();
            foreach (var contextInfo in GetContexts())
                await CreateContextPropertiesFile(directory, contextInfo);
        }

        private static async Task CreateContextPropertiesFile(DirectoryInfo path, ContextInformation contextInfo)
        {
            //todo: implement and extract!

            // fetch context from contextInfo.SourceUrl
            // ...maybe there needs to happen something here too
            // write cs file
            // profit!
            throw new NotImplementedException();
        }

        private static IEnumerable<ContextInformation> GetContexts()
            => Configuration
                .GetSection("jsonld-context-urls")
                .GetChildren()
                .Select(x => new ContextInformation(x.Key, new Uri(x.Value)));
    }
}
