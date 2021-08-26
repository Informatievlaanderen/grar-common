namespace Be.Vlaanderen.Basisregisters.GrAr.Extracts
{
    using Shaperon;

    public class MetadataDbaseSchema : DbaseSchema
    {
        public const int MetadataMaxLength = 50;
        public const int ValueMaxLength = 50;

        private const string MetadataFieldName = "metadata";
        private const string ValueFieldName = "waarde";

        public DbaseField metadata => Fields[0];
        public DbaseField value => Fields[1];

        public MetadataDbaseSchema() => Fields = new[]
        {
            DbaseField.CreateCharacterField(new DbaseFieldName(MetadataFieldName), new DbaseFieldLength(MetadataMaxLength)),
            DbaseField.CreateCharacterField(new DbaseFieldName(ValueFieldName), new DbaseFieldLength(ValueMaxLength))
        };
    }
}
