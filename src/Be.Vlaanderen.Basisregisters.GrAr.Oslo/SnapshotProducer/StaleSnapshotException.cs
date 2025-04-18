namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class StaleSnapshotException : Exception
    {
        public StaleSnapshotException()
        { }

        /// <inheritdoc />
        private StaleSnapshotException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        public StaleSnapshotException(string message)
            : base(message)
        { }

        public StaleSnapshotException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
