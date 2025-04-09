namespace Be.Vlaanderen.Basisregisters.GrAr.Common.SpatialTools.GeometryCoordinates
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;

    public class PointCoordinatesConverter : JsonConverter<double[]>
    {
        private static readonly object Lock = new object();

        public override void WriteJson(
            JsonWriter writer,
            double[]? point,
            JsonSerializer serializer)
        {
            if (point == null)
                throw new ArgumentNullException(nameof(point));

            lock (Lock)
            {
                if (!serializer.Converters.Any(converter => converter is GeometryCoordinateValueConverter))
                    serializer.Converters.Add(new GeometryCoordinateValueConverter());
            }

            serializer.Serialize(
                writer,
                point.Select(coordinateValue => new PointGeometryCoordinateValue(coordinateValue)),
                typeof(GeometryCoordinateValueConverter));
        }

        public override double[] ReadJson(
            JsonReader reader,
            Type objectType,
            double[]? existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
            => throw new NotImplementedException($"Json deserialization of PointCoordinates is not supported");
    }
}
