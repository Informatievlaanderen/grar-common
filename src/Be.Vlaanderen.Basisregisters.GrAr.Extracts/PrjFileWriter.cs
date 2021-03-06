namespace Be.Vlaanderen.Basisregisters.GrAr.Extracts
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Api.Extract;

    public class PrjFileWriter : ExtractFileWriter
    {
        private static Encoding Encoding => Encoding.ASCII;

        public PrjFileWriter(Stream contentStream)
            : base(Encoding, contentStream)
        { }

        public void Write(ProjectedCoordinateSystem coordinateSystem)
        {
            var content = coordinateSystem
                .GetBytes(Encoding)
                .Concat(Encoding.GetBytes(Environment.NewLine))
                .ToArray();

            Writer.Write(content);
        }
    }
}
