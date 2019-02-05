namespace Be.Vlaanderen.Basisregisters.GrAr.Tests
{
    using FluentAssertions;
    using Legacy;
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Xml;
    using Xunit;

    public class Rfc3339DateTimeOffsetTests
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings();

        public Rfc3339DateTimeOffsetTests()
        {
            SerializerSettings.Converters.Add(new Rfc3339SerializableDateTimeOffsetConverter());
        }

        [Fact]
        public void WhenDeserializingXmlToDateTimeOffsetAndParsingThenEqualsExpectedResult()
        {
            var poco = "<Poco xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"><Versie>2002-08-13T17:32:32+02:00</Versie></Poco>";

            var serializer = new DataContractSerializer(typeof(DeseriliazablePoco));

            using (var contentXmlReader = XmlReader.Create(new StringReader(poco), new XmlReaderSettings { Async = false }))
            {
                var result = (DeseriliazablePoco)serializer.ReadObject(contentXmlReader);
                var versie = DateTimeOffset.Parse(result.Versie);

                versie.Year.Should().Be(2002);
                versie.Month.Should().Be(8);
                versie.Day.Should().Be(13);
                versie.Hour.Should().Be(17);
                versie.UtcDateTime.Hour.Should().Be(15);
                versie.Minute.Should().Be(32);
                versie.Second.Should().Be(32);
                versie.Offset.Should().Be(new TimeSpan(2, 0, 0));
            }
        }

        [Fact]
        public void WhenSerializingToJsonThenExpectCorrectString()
        {
            var poco = new JsonPoco
            {
                Versie = new Rfc3339SerializableDateTimeOffset(new DateTimeOffset(2002, 08, 13, 17, 32, 32, 999, new TimeSpan(2, 0, 0)))
            };

            var result = JsonConvert.SerializeObject(poco, SerializerSettings);
            result.Should().NotBeEmpty();
            result.Should().Be("{\"Versie\":\"2002-08-13T17:32:32.999+02:00\"}");
        }

        [Fact]
        public void GivenDateTimeAsDateHandlingWhenDeserializingToJsonThenExpectCorrectString()
        {
            SerializerSettings.DateParseHandling = DateParseHandling.DateTime;
            var result = JsonConvert.DeserializeObject<JsonPoco>("{\"Versie\":\"2002-08-13T17:32:32+02:00\"}", SerializerSettings);
            var versie = (DateTimeOffset)result.Versie;

            versie.Year.Should().Be(2002);
            versie.Month.Should().Be(8);
            versie.Day.Should().Be(13);
            versie.Hour.Should().Be(17);
            versie.UtcDateTime.Hour.Should().Be(15);
            versie.Minute.Should().Be(32);
            versie.Second.Should().Be(32);
            versie.Offset.Should().Be(new TimeSpan(2, 0, 0));
        }

        [Fact]
        public void GivenDateTimeOffsetAsDateHandlingWhenDeserializingToJsonThenExpectCorrectString()
        {
            SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
            var result = JsonConvert.DeserializeObject<JsonPoco>("{\"Versie\":\"2002-08-13T17:32:32+02:00\"}", SerializerSettings);
            var versie = (DateTimeOffset)result.Versie;

            versie.Year.Should().Be(2002);
            versie.Month.Should().Be(8);
            versie.Day.Should().Be(13);
            versie.Hour.Should().Be(17);
            versie.UtcDateTime.Hour.Should().Be(15);
            versie.Minute.Should().Be(32);
            versie.Second.Should().Be(32);
            versie.Offset.Should().Be(new TimeSpan(2, 0, 0));
        }

        [Fact]
        public void GivenNoneAsDateHandlingWhenDeserializingToJsonThenExpectCorrectString()
        {
            SerializerSettings.DateParseHandling = DateParseHandling.None;
            var result = JsonConvert.DeserializeObject<JsonPoco>("{\"Versie\":\"2002-08-13T17:32:32+02:00\"}", SerializerSettings);
            var versie = (DateTimeOffset)result.Versie;

            versie.Year.Should().Be(2002);
            versie.Month.Should().Be(8);
            versie.Day.Should().Be(13);
            versie.Hour.Should().Be(17);
            versie.UtcDateTime.Hour.Should().Be(15);
            versie.Minute.Should().Be(32);
            versie.Second.Should().Be(32);
            versie.Offset.Should().Be(new TimeSpan(2, 0, 0));
        }

        [DataContract(Name = "Poco", Namespace = "")]
        public class DeseriliazablePoco
        {
            [DataMember]
            public string Versie { get; set; }
        }

        [DataContract(Name = "Poco", Namespace = "")]
        public class JsonPoco
        {
            [DataMember]
            public Rfc3339SerializableDateTimeOffset Versie { get; set; }
        }
    }
}
