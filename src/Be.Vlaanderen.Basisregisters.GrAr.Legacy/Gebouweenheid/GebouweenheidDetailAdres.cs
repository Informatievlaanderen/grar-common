﻿namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Gebouweenheid
{
    using System.Runtime.Serialization;

    [DataContract(Name = "Adres")]
    public class GebouweenheidDetailAdres
    {
        /// <summary>
        /// De identifier van het gekoppelde adres.
        /// </summary>
        [DataMember(Name = "ObjectId", Order = 1)]
        public string ObjectId { get; set; }

        /// <summary>
        /// De URL die de details van de meest recente versie van het gekoppelde adres weergeeft.
        /// </summary>
        [DataMember(Name = "Detail", Order = 2)]
        public string Detail { get; set; }

        public GebouweenheidDetailAdres(string objectId, string detail)
        {
            ObjectId = objectId;
            Detail = detail;
        }
    }
}