namespace Be.Vlaanderen.Basisregisters.GrAr.Extracts
{
    using Shaperon;
    using System.IO;
    using System.Text;
    using Api.Extract;

    public class ShpFileWriter : ExtractFileWriter
    {
        private static Encoding Encoding => Encoding.ASCII;

        public ShpFileWriter(ShapeFileHeader header, Stream writeStream)
            : base(Encoding, writeStream) => header.Write(Writer);

        public void Write(ShapeRecord record) => record.Write(Writer);
    }
}
