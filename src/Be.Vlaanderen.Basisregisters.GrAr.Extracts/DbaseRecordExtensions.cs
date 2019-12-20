namespace Be.Vlaanderen.Basisregisters.GrAr.Extracts
{
    using System.IO;
    using System.Text;
    using Shaperon;

    public static class DbaseRecordExtensions
    {
        public static void FromBytes(this DbaseRecord record, byte[] bytes, Encoding encoding)
        {
            using (var input = new MemoryStream(bytes))
            using (var reader = new BinaryReader(input, encoding))
                record.Read(reader);
        }

        public static byte[] ToBytes(this DbaseRecord record, Encoding encoding)
        {
            using (var output = new MemoryStream())
            using (var writer = new BinaryWriter(output, encoding))
            {
                record.Write(writer);
                writer.Flush();
                return output.ToArray();
            }
        }
    }
}
