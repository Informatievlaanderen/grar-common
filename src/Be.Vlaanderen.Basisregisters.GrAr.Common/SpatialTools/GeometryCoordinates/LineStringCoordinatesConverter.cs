namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System;
    using System.Linq;
    using Common.SpatialTools.GeometryCoordinates;
    using Newtonsoft.Json;

    public class LineStringCoordinatesConverter : JsonConverter<double[][]>
    {
        private static readonly object Lock = new object();

        public override void WriteJson(
            JsonWriter writer,
            double[][]? lineString,
            JsonSerializer serializer)
        {
            if(lineString == null)
                throw new ArgumentNullException(nameof(lineString));

            lock (Lock)
            {
                if (!serializer.Converters.Any(converter => converter is GeometryCoordinateValueConverter))
                    serializer.Converters.Add(new GeometryCoordinateValueConverter());
            }

            serializer.Serialize(
                writer,
                lineString.Select(points =>
                    points.Select(coordinateValue => new LineStringGeometryCoordinateValue(coordinateValue))),
                typeof(GeometryCoordinateValueConverter));
        }

        public override double[][] ReadJson(
            JsonReader reader,
            Type objectType,
            double[][]? existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
            => throw new NotImplementedException($"Json deserialization of LineStringCoordinates is not supported");
    }
}
