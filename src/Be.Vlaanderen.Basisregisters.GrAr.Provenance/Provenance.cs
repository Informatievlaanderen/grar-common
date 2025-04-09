namespace Be.Vlaanderen.Basisregisters.GrAr.Provenance
{
    using System;
    using System.Collections.Generic;
    using AggregateSource;
    using NodaTime;

    public class Provenance : ValueObject<Provenance>
    {
        public static readonly string ProvenanceMetadataKey = "Provenance";

        public Instant Timestamp { get; }
        public Application Application { get; }
        public Modification Modification { get; }
        public Operator Operator { get; }
        public Organisation Organisation { get; }
        public Reason Reason { get; }

        public Provenance(
            Instant timestamp,
            Application application,
            Reason reason,
            Operator @operator,
            Modification modification,
            Organisation organisation)
        {
            Timestamp = timestamp;
            Application = application;
            Reason = reason;
            Modification = modification;
            Operator = @operator;
            Organisation = organisation;
        }

        public static Provenance FromDictionary(IDictionary<string, object> dictionary)
        {
            var ignoreCaseDictionary = new Dictionary<string, object>(dictionary, StringComparer.OrdinalIgnoreCase);

            var timestamp = (Instant)ignoreCaseDictionary[nameof(Timestamp)];
            var application = (Application)Enum.Parse(typeof(Application), ignoreCaseDictionary[nameof(Application)].ToString(), true);
            var modification = (Modification)Enum.Parse(typeof(Modification), ignoreCaseDictionary[nameof(Modification)].ToString(), true);
            var organisation = (Organisation)Enum.Parse(typeof(Organisation), ignoreCaseDictionary[nameof(Organisation)].ToString(), true);

            var reason = ignoreCaseDictionary.ContainsKey(nameof(Reason)) && ignoreCaseDictionary[nameof(Reason)] != null
                ? new Reason(ignoreCaseDictionary[nameof(Reason)].ToString())
                : new Reason(string.Empty);

            var @operator = ignoreCaseDictionary.ContainsKey(nameof(Operator)) && ignoreCaseDictionary[nameof(Operator)] != null
                ? new Operator(ignoreCaseDictionary[nameof(Operator)].ToString())
                : new Operator(string.Empty);

            return new Provenance(
                timestamp,
                application,
                reason,
                @operator,
                modification,
                organisation);
        }

        public IDictionary<string, object?> ToDictionary() => new Dictionary<string, object?>
        {
            {nameof(Application), Application},
            {nameof(Modification), Modification},
            {nameof(Timestamp), Timestamp},
            {nameof(Operator), Operator.ToString()},
            {nameof(Organisation), Organisation},
            {nameof(Reason), Reason.ToString()}
        };

        public IEnumerable<object> GetIdentityFields() => Reflect();

        protected override IEnumerable<object> Reflect()
        {
            yield return Application;
            yield return Modification;
            yield return Timestamp.ToString();
            yield return Operator;
            yield return Organisation;
            yield return Reason;
        }
    }
}
