namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Api
{
    using System;
    using System.Runtime.Serialization;

    public partial class IdempotentCommandHandlerModule
    {
        [Serializable]
        public sealed class InvalidCommandException : Exception
        {
            public InvalidCommandException()
            { }

            public InvalidCommandException(string message)
                : base(message)
            { }

            public InvalidCommandException(string message, Exception innerException)
                : base(message, innerException)
            { }


            /// <inerhitdoc />
            private InvalidCommandException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            { }
        }
    }
}
