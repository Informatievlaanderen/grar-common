namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.GeometryCoordinates
{
    using FluentAssertions;
    using Legacy.SpatialTools;
    using Newtonsoft.Json;
    using Xunit;

    public class WhenSerializingAnObjectWithGeometryCoordinates
    {
        [Fact]
        public void ThenTheCoordinateValuesAreFormattedCorrect()
        {
            var coordinates = new CoordinatesSerializationTestModel
            {
                DefaultDoubleFormat = 0.1,
                CoordinateValue = new PointGeometryCoordinateValue(3.1234567890138493),
                PointCoordinates = new[] { 5.2, 2.20192302932029399020, -0.0160097 },
                PolygonCoordinates = new[]
                    {
                        new []
                        {
                            new []{3209.1, -83.013, 0.135000003829},
                            new []{5.3, 2.20192302932029399020}
                        },
                        new []
                        {
                            new []{5.21}
                        }
                    },
                DoubleFormatIsStillDefault = 0.2,
            };


            var expectedJson = ("{" +
                "\"DefaultDoubleFormat\": 0.1" +
                ",\"CoordinateValue\": 3.12" +
                ",\"PointCoordinates\":[ 5.20, 2.20, -0.02 ]" +
                ",\"PolygonCoordinates\":[" +
                    "[" +
                        "[ 3209.10000000000, -83.01300000000, 0.13500000383], [ 5.30000000000, 2.20192302932 ]" +
                    "]," +
                    "[" +
                        "[ 5.21000000000 ]" +
                    "]" +
                "]" +
                ",\"DoubleFormatIsStillDefault\": 0.2" +
            "}").Replace(" ", string.Empty);

            JsonConvert.SerializeObject(coordinates, Formatting.None)
                .Should()
                .Be(expectedJson);
        }

        private class CoordinatesSerializationTestModel
        {
            public double DefaultDoubleFormat { get; set; }

            [JsonConverter(typeof(GeometryCoordinateValueConverter))]
            public GeometryCoordinateValue CoordinateValue { get; set; }

            [JsonConverter(typeof(PointCoordinatesConverter))]
            public double[] PointCoordinates { get; set; }

            [JsonConverter(typeof(PolygonCoordinatesConverter))]
            public double[][][] PolygonCoordinates { get; set; }

            public double DoubleFormatIsStillDefault { get; set; }
        }
    }
}
