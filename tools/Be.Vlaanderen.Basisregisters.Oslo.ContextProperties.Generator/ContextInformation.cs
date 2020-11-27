namespace Be.Vlaanderen.Basisregisters.Oslo.ContextProperties.Generator
{
    using System;

    public class ContextInformation
    {
        public string Name { get; }
        public Uri SourceUrl { get; }

        public ContextInformation(string name, Uri sourceUrl)
        {
            Name = name;
            SourceUrl = sourceUrl;
        }
    }
}
