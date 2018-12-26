namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Xunit
{
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;
    using Processing;
    using Processing.Api;

    public class UnitTestGenerator : IApiProxy
    {
        private IUnitTestGeneratorConfig _config;
        private JsonSerializer _serializer;

        public UnitTestGenerator(JsonSerializer serializer, IUnitTestGeneratorConfig config)
        {
            _serializer = serializer;
            _config = config;
        }

        private void GenerateUnitTest<TKey>(KeyImport<TKey> import)
        {
            var fileName = $"{_config.GetClassName(import.Key)}.cs";
            using (var writer = new StreamWriter(Path.Combine(_config.BasePath, fileName)))
            {
                new UnitTestWriter(_serializer, writer, _config).WriteTest(import).Wait();
            }
        }

        public void ImportBatch<TKey>(IEnumerable<KeyImport<TKey>> imports)
        {
            foreach (var import in imports)
            {
                GenerateUnitTest(import);
            }
        }
    }
}
