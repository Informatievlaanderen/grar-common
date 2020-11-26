namespace Be.Vlaanderen.Basisregisters.Oslo.ContextProperties.Generator
{
    using System;
    using System.IO;
    using Microsoft.Extensions.Configuration;

    public class ContextPropertiesDirectory 
    {
        private const string ApplicationSourceDirectory = "Be.Vlaanderen.Basisregisters.Oslo.ContextProperties.Generator";
        private readonly DirectoryInfo _directory;

        public ContextPropertiesDirectory(IConfiguration configuration)
        {
            var applicationSourceDirectory = FindApplicationSourceDirectory();
            _directory = CreateContextPropertiesDirectory(applicationSourceDirectory, configuration);
        }

        public VersionDirectory CreateVersionDirectory()
            => new VersionDirectory(this);

        private static DirectoryInfo CreateContextPropertiesDirectory(DirectoryInfo directory, IConfiguration configuration)
        {
            return directory
                .Parent // tools directory
                ?.Parent // repository directory
                ?.CreateSubdirectory(configuration["context-properties-output-path"])
                ?? throw new IOException("Unable to create ContextProperties directory");
        }

        private static DirectoryInfo FindApplicationSourceDirectory()
        {
            var directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory ?? throw new IOException("Application directory is not defined"));

            while (directory.Name != ApplicationSourceDirectory)
                directory = directory.Parent ?? throw new IOException($"Could not find Directory {ApplicationSourceDirectory}, Application directory {AppDomain.CurrentDomain.BaseDirectory}");

            return directory;
        }

        public static implicit operator DirectoryInfo(ContextPropertiesDirectory directory)
            => directory._directory;
    }
}
