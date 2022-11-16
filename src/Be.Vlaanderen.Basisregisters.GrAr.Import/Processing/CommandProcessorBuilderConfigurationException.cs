namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class CommandProcessorBuilderConfigurationException : ApplicationException
    {
        public CommandProcessorBuilderConfigurationException(string s)
            : base(s)
        { }

        private CommandProcessorBuilderConfigurationException(SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        { }
    }
}
