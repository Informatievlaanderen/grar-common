namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools.GeometryTypes
{
    using System;
    using Newtonsoft.Json;

    public class GeometryCoordinateValueConverter : JsonConverter<GeometryCoordinateValue>
    {
        public override void WriteJson(
            JsonWriter writer,
            GeometryCoordinateValue value,
            JsonSerializer serializer)
            => writer.WriteRawValue(value.ToString());

        public override GeometryCoordinateValue ReadJson(
            JsonReader reader,
            Type objectType,
            GeometryCoordinateValue existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
            => throw new NotImplementedException($"Json deserialization of {nameof(GeometryCoordinateValue)} is not supported");
    }
}
