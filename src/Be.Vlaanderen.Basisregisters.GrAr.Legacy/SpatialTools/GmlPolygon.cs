namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Een GML3 polygoon.
    /// </summary>
    [DataContract(Namespace = "")]
    public class GmlPolygon
    {
        [DataMember(Name = "exterior")]
        public RingProperty Exterior { get; set; }

        [DataMember(Name = "interior")]
        public List<RingProperty> Interior { get; set; }
    }
}
