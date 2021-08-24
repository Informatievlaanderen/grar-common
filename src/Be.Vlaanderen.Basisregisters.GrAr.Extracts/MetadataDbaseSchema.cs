namespace Be.Vlaanderen.Basisregisters.GrAr.Extracts
{
    using Shaperon;

    public class MetadataDbaseSchema : DbaseSchema
    {
        public const int MetadataMaxLength = 50;
        public const int ValueMaxLength = 50;

        public DbaseField metadata => Fields[0];
        public DbaseField value => Fields[1];

        public MetadataDbaseSchema() => Fields = new[]
        {
            DbaseField.CreateCharacterField(new DbaseFieldName(nameof(metadata)), new DbaseFieldLength(MetadataMaxLength)),
            DbaseField.CreateCharacterField(new DbaseFieldName(nameof(value)), new DbaseFieldLength(ValueMaxLength))
        };
    }
}
