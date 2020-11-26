namespace Be.Vlaanderen.Basisregisters.Oslo.ContextProperties.Generator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class ContextPropertiesFileBuilder
    {
        private readonly DirectoryInfo _directory;
        private readonly string _version;

        public ContextPropertiesFileBuilder(IConfiguration configuration)
        {
            var contextPropertiesDirectory = new ContextPropertiesDirectory(configuration);
            var versionDirectory = contextPropertiesDirectory.CreateVersionDirectory();
            _directory = versionDirectory;
            _version = versionDirectory.Version;
        }

        public async Task CreateContentPropertiesFile(
            ContextInformation contextInfo,
            CancellationToken cancellationToken)
        {
            var context = await new ContextSource(contextInfo.SourceUrl).Fetch(cancellationToken);
            var contextProperties = DetermineContextProperties(context);

            var fileName = Path.Combine(_directory.FullName, contextInfo.Name + ".cs");
            await File.WriteAllTextAsync(fileName, BuildContent(contextInfo, contextProperties), cancellationToken);
        }

        private static IEnumerable<ContextPropertyDefinition> DetermineContextProperties(JObject jObject)
        {
            var objectProperties = new List<ContextPropertyDefinition>();
            var objectToParse = jObject.ContainsKey("@context")
                ? jObject["@context"]
                : jObject;

            foreach (var pair in objectToParse ?? throw new ApplicationException($"Context is not found in: {jObject}"))
            {
                if (pair is JProperty property)
                {
                    var reference = property.Value.Count() > 1
                        ? property.Value["@id"]?.ToString() ?? ""
                        : property.Value.ToString();

                    objectProperties.Add(new ContextPropertyDefinition(property.Name, reference));
                }
            }

            return objectProperties;
        }

        private string BuildContent(
            ContextInformation contextInfo,
            IEnumerable<ContextPropertyDefinition> contextProperties)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.ContextProperties.{_version}");
            builder.AppendLine("{");
            builder.AppendLine($@"{Indent(1)}///<summary>source: <a href=""{contextInfo.SourceUrl}"">{contextInfo.SourceUrl}</a></summary>");
            builder.AppendLine($"{Indent(1)}public class {contextInfo.Name}");
            builder.AppendLine($"{Indent(1)}{{");

            using (var enumerator = contextProperties.GetEnumerator())
            {
                var next = enumerator.MoveNext();
                while (next)
                {
                    var property = enumerator.Current;
                    builder.AppendLine($@"{Indent(2)}///<summary>source: <a href=""{property.Reference}"">{property.Reference}</a></summary>");
                    builder.AppendLine($@"{Indent(2)}public const string {property.Name} = ""{property.Value}"";");

                    next = enumerator.MoveNext();
                    if (next)
                        builder.AppendLine();
                }
            }

            builder.AppendLine($"{Indent(1)}}}");
            builder.AppendLine("}");

            return builder.ToString();
        }

        private static string Indent(int amount)
            => "".PadLeft(4 * amount);
    }
}
