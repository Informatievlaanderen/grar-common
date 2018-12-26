namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing
{
    public class KeyImport<TKey>
    {
        public TKey Key { get; }
        public ImportCommand[] Commands { get; }

        public KeyImport(TKey key,
            params ImportCommand[] commands)
        {
            Key = key;
            Commands = commands;
        }
    }
}
