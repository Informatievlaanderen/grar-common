namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Extracts
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentAssertions;
    using GrAr.Extracts;
    using Xunit;

    public class WhenGeneratingABelgeLambert1972ProjectionCoordinateSystemFile
    {
        private readonly ProjectedCoordinateSystemStream _projectionCoordinates;

        public WhenGeneratingABelgeLambert1972ProjectionCoordinateSystemFile()
        {
            using (var memoryStream = new MemoryStream())
            {
                ExtractBuilder
                    .CreateProjectedCoordinateSystemFile("test", ProjectedCoordinateSystem.Belge_Lambert_1972)
                    .WriteTo(memoryStream, CancellationToken.None);

                _projectionCoordinates = new ProjectedCoordinateSystemStream(memoryStream);
            }
        }

        [Fact]
        public void ItShouldEndWithANewLine()
        {
            _projectionCoordinates
                .Content
                .Should()
                .EndWith(Environment.NewLine);
        }

        [Fact]
        public void ItShouldContainTheBelgeLambert1972Parameters()
        {
            _projectionCoordinates
                .Content
                .TrimEnd()
                .Should()
                .Be(ProjectedCoordinateSystem.Belge_Lambert_1972.ToString());
        }

        [Fact]
        public async Task ItShouldMatchTheReferenceFile()
        {
            var referenceFile = new FileInfo(Path.Join(Environment.CurrentDirectory, "Extracts", "References","Belge_Lambert_1972.prj"));
            if (!referenceFile.Exists)
                throw new ArgumentException($"Reference file {referenceFile.FullName} not found");

            var referenceBytes = await File.ReadAllBytesAsync(referenceFile.FullName, CancellationToken.None);

            _projectionCoordinates
                .Bytes
                .Should()
                .Equal(referenceBytes);
        }

        public class ProjectedCoordinateSystemStream : MemoryStream
        {
            public ProjectedCoordinateSystemStream(MemoryStream stream)
                : base(stream.GetBuffer(), false)
            {
                using (var reader = new StreamReader(this))
                    Content = reader.ReadToEnd();
            }

            public string Content { get; }
            public byte[] Bytes => ToArray();
        }
    }
}
