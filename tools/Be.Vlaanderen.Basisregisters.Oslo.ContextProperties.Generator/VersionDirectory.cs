namespace Be.Vlaanderen.Basisregisters.Oslo.ContextProperties.Generator
{
    using System;
    using System.IO;
    using System.Linq;

    public class VersionDirectory
    {
        private readonly DirectoryInfo _contextPropertiesDirectory;

        public string Version { get; }

        public VersionDirectory(ContextPropertiesDirectory contextPropertiesDirectory)
        {
            _contextPropertiesDirectory = contextPropertiesDirectory;
            Version = DetermineVersion();
        }

        private string DetermineVersion()
        {
            var versionDirectory = DateTime.Today.ToString("vyyyyMMdd");
            var index = 0;
            while (_contextPropertiesDirectory.GetDirectories(versionDirectory).Any())
            {
                index++;
                versionDirectory = $"{versionDirectory.Split('_')[0]}_{index:00}";
            }

            return versionDirectory;
        }

        public static implicit operator DirectoryInfo(VersionDirectory versionDirectory)
            => versionDirectory
                ._contextPropertiesDirectory
                .CreateSubdirectory(versionDirectory.Version);
    }
}
