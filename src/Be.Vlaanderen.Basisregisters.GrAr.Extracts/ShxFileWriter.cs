namespace Be.Vlaanderen.Basisregisters.GrAr.Extracts
{
    using Shaperon;
    using System.IO;
    using System.Text;
    using Api.Extract;

    public class ShxFileWriter : ExtractFileWriter
    {
        private static Encoding Encoding => Encoding.ASCII;

        public ShxFileWriter(ShapeFileHeader header, Stream writeStream)
            : base(Encoding, writeStream) => header.Write(Writer);

        public void Write(ShapeIndexRecord record) => record.Write(Writer);
    }
}
