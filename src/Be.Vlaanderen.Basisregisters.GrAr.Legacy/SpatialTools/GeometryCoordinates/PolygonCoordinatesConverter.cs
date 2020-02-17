namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;

    public class PolygonCoordinatesConverter : JsonConverter<double[][][]>
    {
        public override void WriteJson(
            JsonWriter writer,
            double[][][] polygon,
            JsonSerializer serializer)
        {
            if (!serializer.Converters.Any(converter => converter is GeometryCoordinateValueConverter))
                serializer.Converters.Add(new GeometryCoordinateValueConverter());

            var typedPolygon =
                polygon.Select(
                    ring => ring.Select(
                        point => point.Select(
                            coordinateValue => new PolygonGeometryCoordinateValue(coordinateValue))));

            serializer.Serialize(
                writer,
                typedPolygon,
                typeof(GeometryCoordinateValueConverter));
        }

        public override double[][][] ReadJson(
            JsonReader reader,
            Type objectType,
            double[][][] existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
            => throw new NotImplementedException("Json deserialization of PolygonCoordinates is not supported");
    }
}
