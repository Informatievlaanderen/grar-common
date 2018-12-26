namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing
{
    using System.Globalization;
    using System.IO;
    using System.Text;
    using Newtonsoft.Json;

    public static class JsonSerializerExtensions
    {
        public static string Serialize(this JsonSerializer jsonSerializer,
            object value)
        {
            var stringWriter = new StringWriter(new StringBuilder(256), CultureInfo.InvariantCulture);
            using (var jsonTextWriter = new JsonTextWriter(stringWriter))
            {
                jsonTextWriter.Formatting = jsonSerializer.Formatting;
                jsonSerializer.Serialize(jsonTextWriter, value, value.GetType());
            }

            return stringWriter.ToString();
        }
    }
}
