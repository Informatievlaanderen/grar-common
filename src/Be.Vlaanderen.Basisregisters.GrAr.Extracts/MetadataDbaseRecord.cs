namespace Be.Vlaanderen.Basisregisters.GrAr.Extracts
{
    using Shaperon;

    public class MetadataDbaseRecord : DbaseRecord
    {
        public static readonly MetadataDbaseSchema Schema = new MetadataDbaseSchema();

        public DbaseCharacter metadata { get; }
        public DbaseCharacter value { get; }

        public MetadataDbaseRecord()
        {
            metadata = new DbaseCharacter(Schema.metadata);
            value = new DbaseCharacter(Schema.value);

            Values = new DbaseFieldValue[]
            {
                metadata,
                value
            };
        }
    }
}
