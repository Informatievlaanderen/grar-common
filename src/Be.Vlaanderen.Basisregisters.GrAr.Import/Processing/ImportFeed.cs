namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing
{
    public class ImportFeed
    {
        private string _lowercasedName = string.Empty;

        public string? Name
        {
            get => _lowercasedName;
            set => _lowercasedName = value?.ToLowerInvariant().Trim() ?? string.Empty;
        }

        public static explicit operator ImportFeed(string? name) => new ImportFeed { Name = name };
    }
}
