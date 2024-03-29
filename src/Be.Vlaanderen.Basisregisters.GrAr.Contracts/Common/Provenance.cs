namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.Common
{
    public class Provenance
    {
        /// <summary>
        /// Format ISO-8601
        /// </summary>
        public string Timestamp { get; }
        public string Application { get; }
        public string Modification { get; }
        public string Organisation { get; }
        public string Reason { get; }

        public Provenance(string timestamp,
            string application,
            string modification,
            string organisation,
            string reason)
        {
            Timestamp = timestamp;
            Application = application;
            Modification = modification;
            Organisation = organisation;
            Reason = reason;
        }
    }
}
