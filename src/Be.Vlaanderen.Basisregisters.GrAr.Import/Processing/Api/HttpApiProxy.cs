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
        
        private void Using(Action<HttpClient> executeCall)
        {
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

                executeCall(client);
            }
        }

        public void ImportBatch<TKey>(IEnumerable<KeyImport<TKey>> imports)
        {
            var json = _serializer.Serialize(imports.Select(i => i.Commands));

            Using(client =>
            {

                _logger.LogDebug("Posting to {baseUrl}", _config.BaseUrl);
                _logger.LogTrace($"Payload:{Environment.NewLine}{{json}}", json);
                var watch = Stopwatch.StartNew();
                var response = client
                    .PostAsync(
                        _config.ImportEndpoint,
                        new StringContent(json, Encoding.UTF8, "application/json")
                    )
                    .GetAwaiter()
                    .GetResult();
                watch.Stop();
                _logger.LogDebug("Post to {baseUrl} was {statusCode} (took:{duration}ms)", _config.BaseUrl, response.StatusCode, watch.ElapsedMilliseconds);
                response.EnsureSuccessStatusCode();
            });
        }

        public ICommandProcessorOptions<TKey> InitialiseImport<TKey>(
            ImportOptions options,
            ICommandProcessorBatchConfiguration<TKey> configuration)
        {
            ImportBatchStatus batchStatus = null;
            Using(client =>
            {
                _logger.LogDebug("Getting batch status from {baseUrl}", _config.BaseUrl);
                var watch = Stopwatch.StartNew();
                var response = client
                    .GetAsync(_config.ImportBatchStatusEndpoint)
                    .GetAwaiter()
                    .GetResult();
                watch.Stop();
                _logger.LogDebug(
                    "Get from {baseUrl} was {statusCode} (took:{duration}ms)",
                    _config.BaseUrl,
                    response.StatusCode,
                    watch.ElapsedMilliseconds);

                var content = response
                    .EnsureSuccessStatusCode()
                    .Content
                    .ReadAsStringAsync()
                    .GetAwaiter()
                    .GetResult() ?? string.Empty;

                batchStatus = JsonConvert.DeserializeObject<ImportBatchStatus>(content);
            });

            var processorOptions = options.CreateProcessorOptions(batchStatus, configuration);
            PostImportBatchStatus(processorOptions, Batch.Start);

            return processorOptions;
        }

        public void FinaliseImport<TKey>(ICommandProcessorOptions<TKey> options)
        {
            PostImportBatchStatus(options, Batch.Completed);
        }

        private static class Batch
        {
            public static bool Start => false;
            public static bool Completed => true;
        }

        private void PostImportBatchStatus<TKey>(ICommandProcessorOptions<TKey> options, bool importCompleted)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            var json = _serializer.Serialize(
                new ImportBatchStatus
                {
                    From = options.From,
                    Until = options.Until,
                    Completed = importCompleted
                });

            Using(client =>
            {
                _logger.LogDebug("Posting batch status from {baseUrl}", _config.BaseUrl);
                var watch = Stopwatch.StartNew();
                var response = client
                    .PostAsync(
                        _config.ImportBatchStatusEndpoint,
                        new StringContent(json, Encoding.UTF8, "application/json")
                    )
                    .GetAwaiter()
                    .GetResult();
                watch.Stop();
                _logger.LogDebug(
                    "Post from {baseUrl} was {statusCode} (took:{duration}ms)",
                    _config.BaseUrl,
                    response.StatusCode,
                    watch.ElapsedMilliseconds);

                response.EnsureSuccessStatusCode();
            });
        }
    }
}
