namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools.GeometryTypes
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;

    public class PointCoordinatesConverter : JsonConverter<double[]>
    {
        public override void WriteJson(
            JsonWriter writer,
            double[] point,
            JsonSerializer serializer)
        {
            if (!serializer.Converters.Any(converter => converter is GeometryCoordinateValueConverter))
                serializer.Converters.Add(new GeometryCoordinateValueConverter());

            serializer.Serialize(
                writer,
                point.Select(coordinateValue => new GeometryCoordinateValue(coordinateValue)),
                typeof(GeometryCoordinateValueConverter));
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
