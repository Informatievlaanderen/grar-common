namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System;
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
            serializer.Serialize(writer, GmlHelper.ToGmlPointString(point), typeof(string));
        }

        public override double[] ReadJson(
            JsonReader reader,
            Type objectType,
            double[] existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader.Value is string gml)
                return GmlHelper.ParseGmlPointString(gml);
            return default;
        }
    }
}
