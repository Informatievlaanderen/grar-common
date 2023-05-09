namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.SnapshotProducer
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using AspNetCore.Mvc.Formatters.Json;
    using Newtonsoft.Json;

    public interface IOsloProxy
    {
        Task<OsloResult> GetSnapshot(string persistentLocal, CancellationToken cancellationToken);
    }

    public sealed class OsloProxy : IOsloProxy
    {
        private readonly HttpClient _httpClient;

        public OsloProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OsloResult> GetSnapshot(string persistentLocal, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_httpClient.BaseAddress}/{persistentLocal}");
            request.Headers.Add("Cache-Control", "no-cache");
            request.Headers.Add("Accept", "application/ld+json");

            var response = await _httpClient.SendAsync(request, cancellationToken);

            response.EnsureSuccessStatusCode();

            var jsonContent = await response.Content.ReadAsStringAsync(cancellationToken);

            var osloResult = JsonConvert.DeserializeObject<OsloResult>(jsonContent, new JsonSerializerSettings().ConfigureDefaultForApi());

            if (osloResult is null)
            {
                throw new JsonSerializationException();
            }

            osloResult.JsonContent = jsonContent;
            osloResult.ETag = response.Headers.ETag?.Tag.Trim('"');

            return osloResult;
        }
    }
}
