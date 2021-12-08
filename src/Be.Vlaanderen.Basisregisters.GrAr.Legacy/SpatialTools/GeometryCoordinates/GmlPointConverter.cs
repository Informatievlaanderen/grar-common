namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;

    public class GmlPointConverter : JsonConverter<double[]>
    {
        public override void WriteJson(
            JsonWriter writer,
            double[] point,
            JsonSerializer serializer)
        {
            if (point.Length < 1)
                return;

            var coordinates = point.Select(coordinateValue => coordinateValue.ToPointGeometryCoordinateValueFormat());
            var glm = $"<gml:Point srsName='https://www.opengis.net/def/crs/EPSG/0/31370'><gml:pos>{string.Join(' ', coordinates)}</gml:pos></gml:Point>";
            serializer.Serialize(writer, glm, typeof(string));
        }

        public override double[] ReadJson(
            JsonReader reader,
            Type objectType,
            double[] existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
            => throw new NotImplementedException($"Json deserialization of PointCoordinates is not supported");
    }
}
