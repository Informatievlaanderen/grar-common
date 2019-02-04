namespace Be.Vlaanderen.Basisregisters.GrAr.Tests
{
    using FluentAssertions;
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Xml;
    using Legacy;
    using Xunit;

    public class Rfc3339DateTimeOffsetTests
    {
        [Fact]
        public void WhenDeserializingToDateTimeOffset()
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

        [DataContract(Name = "Poco", Namespace = "")]
        public class DeseriliazablePoco
        {
            [DataMember]
            public string Versie { get; set; }
        }

        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
