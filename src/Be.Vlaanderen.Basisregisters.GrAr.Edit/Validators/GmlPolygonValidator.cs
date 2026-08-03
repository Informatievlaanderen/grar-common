namespace Be.Vlaanderen.Basisregisters.GrAr.Edit.Validators
{
    using System;
    using System.Xml;
    using Common;
    using Common.NetTopology;
    using NetTopologySuite.Geometries;
    using NetTopologySuite.IO.GML2;
    using NetTopologySuite.Operation.Valid;

    public static class GmlPolygonValidator
    {
        private const string GmlVersionAttributeValue = "http://www.opengis.net/gml/3.2";

        /// <summary>
        /// Lambert-1972 only
        /// </summary>
        [Obsolete("Use IsValidPolygon(string, out Polygon?) instead.")]
        public static bool IsValid(string? gml, GMLReader gmlReader)
        {
            return IsValid(gml, gmlReader, null);
        }

        /// <summary>
        /// Lambert-1972 only
        /// </summary>
        [Obsolete("Use IsValidPolygon(string, out Polygon?) instead.")]
        public static bool IsValid(
            string? gml,
            GMLReader gmlReader,
            Func<Geometry, IsValidOp>? isValidOpFactory)
        {
            return IsValid(gml, gmlReader, isValidOpFactory, out _);
        }

        /// <summary>
        /// Lambert-1972 only
        /// </summary>
        /// <param name="gml"></param>
        /// <param name="gmlReader"></param>
        /// <param name="isValidOpFactory"></param>
        /// <param name="polygon"></param>
        /// <returns></returns>
        [Obsolete("Use IsValidPolygon(string, Func<Geometry, IsValidOp>?, out Polygon?) instead.")]
        public static bool IsValid(
            string? gml,
            GMLReader gmlReader,
            Func<Geometry, IsValidOp>? isValidOpFactory,
            out Polygon? polygon)
        {
            polygon = null;
            if (string.IsNullOrEmpty(gml) || !gml.Contains(GmlConstants.GmlVersionAttribute) || !gml.Contains(GmlConstants.SrsNameAttribute))
            {
                return false;
            }

            try
            {
                var geometry = gmlReader.Read(gml);

                if (geometry is Polygon && (isValidOpFactory is null ? geometry.IsValid : isValidOpFactory(geometry).IsValid))
                {
                    polygon = (Polygon)geometry;
                    return true;
                }

                return false;
            }
            catch (XmlException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        /// <summary>
        /// This accepts both Lambert 72 (EPSG 31370) and Lambert 2008 (EPSG 3812), with either an http or an
        /// https srsName, and reads the polygon in the reference system it was sent with.
        /// </summary>
        /// <param name="gml"></param>
        /// <returns></returns>
        public static bool IsValidPolygon(string? gml) => IsValidPolygon(gml, null, out _);

        /// <summary>
        /// This accepts both Lambert 72 (EPSG 31370) and Lambert 2008 (EPSG 3812), with either an http or an
        /// https srsName, and reads the polygon in the reference system it was sent with.
        /// </summary>
        /// <param name="gml"></param>
        /// <param name="isValidOpFactory"></param>
        /// <returns></returns>
        public static bool IsValidPolygon(string? gml, Func<Geometry, IsValidOp>? isValidOpFactory) =>
            IsValidPolygon(gml, isValidOpFactory, out _);

        /// <summary>
        /// The polygon counterpart of <c>GrAr.Edit.Validators.GmlPointValidator.IsValidPoint</c>, which GrAr.Edit does not
        /// provide. This accepts both Lambert 72 (EPSG 31370) and Lambert 2008 (EPSG 3812), with either an http or an
        /// https srsName, and reads the polygon in the reference system it was sent with.
        /// </summary>
        public static bool IsValidPolygon(
            string? gml,
            Func<Geometry, IsValidOp>? isValidOpFactory,
            out Polygon? polygon)
        {
            polygon = null;

            if (string.IsNullOrEmpty(gml) || !gml.Contains(GmlVersionAttributeValue))
            {
                return false;
            }

            // the reference system whitelist is the srsName check
            if (!gml.TryReadSridGml(out var srid))
            {
                return false;
            }

            try
            {
                var geometry = GmlFactory.CreateGmlReader(srid).Read(gml);

                if (geometry is Polygon gmlPolygon
                    && (isValidOpFactory is null ? geometry.IsValid : isValidOpFactory(geometry).IsValid))
                {
                    polygon = gmlPolygon;
                    return true;
                }

                return false;
            }
            catch (XmlException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
    }
}
