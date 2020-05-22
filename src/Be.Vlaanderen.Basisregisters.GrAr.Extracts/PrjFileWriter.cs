namespace Be.Vlaanderen.Basisregisters.GrAr.Extracts
{
    using System.IO;
    using System.Text;
    using Api.Extract;

    public class PrjFileWriter : ExtractFileWriter
    {
        private static Encoding Encoding => Encoding.ASCII;

        public PrjFileWriter(Stream contentStream)
            : base(Encoding, contentStream)
        { }

        public void Write(ProjectedCoordinateSystem coordinateSystem) => coordinateSystem.Write(Writer, Encoding);
    }
}
