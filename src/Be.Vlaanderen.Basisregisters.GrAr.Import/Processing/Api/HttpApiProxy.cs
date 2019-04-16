namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.Api
{
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;

    public class HttpApiProxy : IApiProxy
    {
        private readonly IHttpApiProxyConfig _config;
        private readonly ILogger _logger;
        private readonly JsonSerializer _serializer;

        public HttpApiProxy(
            ILogger logger,
            JsonSerializer serializer,
            IHttpApiProxyConfig config)
        {
            _serializer = serializer;
            _logger = logger;
            _config = config;
        }

        public void ImportBatch<TKey>(IEnumerable<KeyImport<TKey>> imports)
        {
            var json = _serializer.Serialize(imports.Select(i => i.Commands));

            using (var client = new HttpClient { BaseAddress = _config.BaseUrl })
            {
                client.Timeout = TimeSpan.FromMinutes(_config.HttpTimeoutMinutes);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (!string.IsNullOrEmpty(_config.AuthUserName) &&
                    !string.IsNullOrEmpty(_config.AuthPassword))
                {
                    var encodedString = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_config.AuthUserName}:{_config.AuthPassword}"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedString);
                }

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _logger.LogDebug("Posting to {baseUrl}", _config.BaseUrl);
                _logger.LogTrace($"Payload:{Environment.NewLine}{{json}}", json);
                var watch = Stopwatch.StartNew();
                var response = client.PostAsync(_config.ImportEndpoint, content).GetAwaiter().GetResult();
                watch.Stop();
                _logger.LogDebug("Post to {baseUrl} was {statusCode} (took:{duration}ms)", _config.BaseUrl, response.StatusCode, watch.ElapsedMilliseconds);
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
