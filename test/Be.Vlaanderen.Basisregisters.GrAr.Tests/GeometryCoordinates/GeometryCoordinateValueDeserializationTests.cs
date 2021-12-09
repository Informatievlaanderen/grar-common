namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.GeometryCoordinates
{
    using FluentAssertions;
    using Legacy.SpatialTools;
    using Newtonsoft.Json;
    using Xunit;

    public class WhenDeserializingAnObjectWithGml
    {
        [Fact]
        public void ThenTheCoordinateValuesAsExpected()
        {
            var givenJson = "{" +
                            "\"Gml\": \"<gml:Point srsName='https://www.opengis.net/def/crs/EPSG/0/31370'><gml:pos>5.20 2.20 -0.02</gml:pos></gml:Point>\"" +
                            ",\"GmlString\": \"<gml:Point srsName='https://www.opengis.net/def/crs/EPSG/0/31370'><gml:pos>5.20 2.20 -0.02</gml:pos></gml:Point>\"" +
                            "}";

            var expected = new CoordinatesDeserializationTestModel
            {
                Gml = new[] {5.20, 2.20, -0.02},
                GmlString = "<gml:Point srsName='https://www.opengis.net/def/crs/EPSG/0/31370'><gml:pos>5.20 2.20 -0.02</gml:pos></gml:Point>"
            };

            var result = JsonConvert.DeserializeObject<CoordinatesDeserializationTestModel>(givenJson);
            result!.Gml.Should().Contain(expected.Gml);
            result!.GmlString.Should().Be(expected.GmlString);
        }

        private class CoordinatesDeserializationTestModel
        {
            [JsonConverter(typeof(GmlPointConverter))]
            public double[] Gml { get; set; }

            public string GmlString { get; set; }
        }
    }
}
