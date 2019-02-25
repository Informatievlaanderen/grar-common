namespace Be.Vlaanderen.Basisregisters.GrAr.Common.Syndication
{
    using System.IO;
    using System.Runtime.Serialization;
    using System.Xml;

    [DataContract]
    public abstract class SyndicationContentBase
    {
        public string ToXml()
        {
            var serializer = new DataContractSerializer(GetType());

            using (var output = new StringWriter())
            using (var writer = new XmlTextWriter(output) { Formatting = Formatting.Indented })
            {
                serializer.WriteObject(writer, this);
                return output.GetStringBuilder().ToString();
            }
        }
    }
}
