namespace Be.Vlaanderen.Basisregisters.Oslo.ContextProperties.Generator
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;

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
            var contextProperties = new Dictionary<string, string>(); // determine props from context

            var fileName = Path.Combine(_directory.FullName, contextInfo.Name + ".cs");
            await File.WriteAllTextAsync(fileName, BuildContent(contextInfo.Name, contextProperties), cancellationToken);
        }

        private string BuildContent(
            object className,
            IDictionary<string, string> contextProperties)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.ContextProperties.{_version}");
            builder.AppendLine("{");
            builder.AppendLine($"{Indent(1)}public class {className}");
            builder.AppendLine($"{Indent(1)}{{");

            foreach (var property in contextProperties)
                builder.AppendLine($@"{Indent(2)}public const string {property.Key} = ""{property.Value}"";");

            builder.AppendLine($"{Indent(1)}}}");
            builder.AppendLine("}");

            return builder.ToString();
        }

        private static string Indent(int amount)
            => "".PadLeft(4 * amount);
    }
}
