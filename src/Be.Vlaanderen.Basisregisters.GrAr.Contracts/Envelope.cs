namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts
{
    public class Envelope<T>
    {
        /// <summary>
        /// The unique identifier.
        /// The value of this Guid, formatted by using the "D" format specifier as follows
        /// https://docs.microsoft.com/en-us/dotnet/api/system.guid.tostring?view=net-5.0
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the event.
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// Format: ISO8086
        /// </summary>
        public string Timestamp { get; set; }

        /// <summary>
        /// The payload of the event.
        /// </summary>
        public string Payload { get; set; }
    }
}
