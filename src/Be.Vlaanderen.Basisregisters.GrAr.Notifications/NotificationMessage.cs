namespace Be.Vlaanderen.Basisregisters.GrAr.Notifications
{
    using System.Text.Json.Serialization;

    public class NotificationMessage
    {
        [JsonIgnore]
        public string MessageType { get; }
        public string BasisregistersError { get; }
        public string Service { get; }
        public string Warning { get; }

        public NotificationMessage(string messageType, string basisregistersError, string service, NotificationSeverity warning)
        {
            MessageType = messageType;
            BasisregistersError = basisregistersError;
            Service = service;
            Warning = warning.ToString().ToLowerInvariant();
        }
    }
}
