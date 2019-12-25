namespace Be.Vlaanderen.Basisregisters.GrAr.Extracts
{
    using System.IO;
    using System.Text;
    using Shaperon;

    public static class ShapeContentExtensions
    {
        public static ShapeContent FromBytes(this byte[] bytes)
        {
            using (var input = new MemoryStream(bytes))
            using (var reader = new BinaryReader(input))
                return ShapeContent.Read(reader);
        }

        public static ShapeContent FromBytes(this byte[] bytes, Encoding encoding)
        {
            using (var input = new MemoryStream(bytes))
            using (var reader = new BinaryReader(input, encoding))
                return ShapeContent.Read(reader);
        }

        public static byte[] ToBytes(this ShapeContent shape)
        {
            using (var output = new MemoryStream())
            using (var writer = new BinaryWriter(output))
            {
                shape.Write(writer);
                writer.Flush();
                return output.ToArray();
            }
        }

        public static byte[] ToBytes(this ShapeContent shape, Encoding encoding)
        {
            using (var output = new MemoryStream())
            using (var writer = new BinaryWriter(output, encoding))
            {
                shape.Write(writer);
                writer.Flush();
                return output.ToArray();
            }
        }
    }
}
