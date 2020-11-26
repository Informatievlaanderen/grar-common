namespace Be.Vlaanderen.Basisregisters.Oslo.ContextProperties.Generator
{
    public class ContextPropertyDefinition
    {
        public ContextPropertyDefinition(
            string key,
            string reference)
        {
            Name = $"__{key.Replace('.', '_')}";
            Value = key;
            Reference = reference;
        }

        public string Name { get; }
        public string Value { get; }
        public string Reference { get; }
    }
}
