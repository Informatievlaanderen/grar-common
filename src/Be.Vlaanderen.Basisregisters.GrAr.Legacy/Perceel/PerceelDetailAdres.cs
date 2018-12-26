namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.Perceel
{
    using System;
    using System.Runtime.Serialization;

    [DataContract(Name = "Adres", Namespace = "")]
    public class PerceelDetailAdres
    {
        /// <summary>
        /// De identifier van het gekoppelde adres.
        /// </summary>
        [DataMember(Name = "ObjectId", Order = 1)]
        public string ObjectId { get; set; }

        /// <summary>
        /// URL returning the details of the latest version of the coupled address
        /// </summary>
        [DataMember(Name = "Detail", Order = 2)]
        public Uri Detail { get; set; }

        public static PerceelDetailAdres Create(string objectId, Uri detail)
        {
            return new PerceelDetailAdres
            {
                ObjectId = objectId,
                Detail = detail
            };
        }
    }
}
