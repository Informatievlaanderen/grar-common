namespace Be.Vlaanderen.Basisregisters.Oslo.ContextProperties.Generator
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;

    public class ContextSource
    {
        private readonly Uri _contextPath;

        public ContextSource(Uri path) => _contextPath = path;

        public async Task<JObject> Fetch(CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(_contextPath, cancellationToken);
                response.EnsureSuccessStatusCode();
                return JObject.Parse(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
