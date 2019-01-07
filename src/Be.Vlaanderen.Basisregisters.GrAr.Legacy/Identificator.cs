namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy
{
    using System;
    using System.Runtime.Serialization;
    using System.Web;

    /// <summary>
    /// Bevat informatie waarmee een object kan geïdentificeerd worden.
    /// </summary>
    [DataContract(Name = "Identificator", Namespace = "")]
    public class Identificator
    {
        /// <summary>
        /// De persistente identificator van het object (volgens de Vlaamse URI standaard), uniek overheen heel het wereldwijde web.
        /// </summary>
        [DataMember(Name = "Id", Order = 1)]
        public string Id { get; set; }

        /// <summary>
        /// De naamruimte die de bron uniek identificeert overheen heel het wereldwijde web.
        /// </summary>
        [DataMember(Name = "Naamruimte", Order = 2)]
        public string Naamruimte { get; set; }

        /// <summary>
        /// De identificator van het object, uniek overheen heel het wereldwijde web.
        /// </summary>
        [DataMember(Name = "ObjectId", Order = 3)]
        public string ObjectId { get; set; }

        /// <summary>
        /// De versie van het object.
        /// </summary>
        [DataMember(Name = "Versie", Order = 4)]
        public DateTimeOffset? Versie { get; set; }

        public Identificator(string naamruimte, string objectId, DateTimeOffset? versie)
        {
            Naamruimte = naamruimte;
            Id = $"{naamruimte}/{HttpUtility.UrlEncode(objectId)}";
            ObjectId = objectId;
            Versie = versie;
        }
    }
}
