namespace Be.Vlaanderen.Basisregisters.Oslo.ContextProperties.Generator
{
    using System;

    public class ContextInformation
    {
        public ContextInformation(string fileName, Uri sourceUrl)
        {
            FileName = fileName;
            SourceUrl = sourceUrl;
        }

        public string FileName { get; }
        public Uri SourceUrl { get; }
    }
}
