namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.Common
{
    public class Provenance
    {

        /// <summary>
        /// Format ISO-8601
        /// </summary>
        public string Timestamp { get; }

        /// <summary>
        ///
        /// </summary>
        public string Application { get; }

        /// <summary>
        ///
        /// </summary>
        public string Modification { get; }

        /// <summary>
        ///
        /// </summary>
        public string Operator { get; }

        /// <summary>
        ///
        /// </summary>
        public string Organisation { get; }

        /// <summary>
        ///
        /// </summary>
        public string Reason { get; }

        public Provenance(string timestamp,
            string application,
            string modification,
            string @operator,
            string organisation,
            string reason)
        {
            Timestamp = timestamp;
            Application = application;
            Modification = modification;
            Operator = @operator;
            Organisation = organisation;
            Reason = reason;
        }
    }
}
