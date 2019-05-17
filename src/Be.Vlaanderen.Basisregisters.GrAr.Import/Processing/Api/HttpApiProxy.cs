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
        protected readonly IHttpApiProxyConfig Config;
        protected readonly ILogger Logger;
        protected readonly JsonSerializer Serializer;

        public HttpApiProxy(
            ILogger logger,
            JsonSerializer serializer,
            IHttpApiProxyConfig config)
        {
            Serializer = serializer;
            Logger = logger;
            Config = config;
        }

        protected void Using(Action<HttpClient> executeCall)
        {
            using (var client = new HttpClient { BaseAddress = Config.BaseUrl })
            {
                client.Timeout = TimeSpan.FromMinutes(Config.HttpTimeoutMinutes);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));

                if (!string.IsNullOrEmpty(Config.AuthUserName) &&
                    !string.IsNullOrEmpty(Config.AuthPassword))
                {
                    var encodedString = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Config.AuthUserName}:{Config.AuthPassword}"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedString);
                }

                executeCall(client);
            }
        }

        public void ImportBatch<TKey>(IEnumerable<KeyImport<TKey>> imports)
        {
            var json = Serializer.Serialize(imports.Select(i => i.Commands));

            Using(client =>
            {

                Logger.LogDebug("Posting to {baseUrl}", Config.BaseUrl);
                Logger.LogTrace($"Payload:{Environment.NewLine}{{json}}", json);
                var watch = Stopwatch.StartNew();
                var response = client
                    .PostAsync(
                        Config.ImportEndpoint,
                        new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json)
                    )
                    .GetAwaiter()
                    .GetResult();
                watch.Stop();
                Logger.LogDebug("Post to {baseUrl} was {statusCode} (took:{duration}ms)", Config.BaseUrl, response.StatusCode, watch.ElapsedMilliseconds);
                response.EnsureSuccessStatusCode();
            });
        }

        public ICommandProcessorOptions<TKey> InitializeImport<TKey>(
            ImportOptions options,
            ICommandProcessorBatchConfiguration<TKey> configuration)
        {
            ImportBatchStatus batchStatus = null;
            Using(client =>
            {
                Logger.LogDebug("Getting batch status from {baseUrl}", Config.BaseUrl);
                var watch = Stopwatch.StartNew();
                var response = client
                    .GetAsync(Config.ImportBatchStatusEndpoint)
                    .GetAwaiter()
                    .GetResult();
                watch.Stop();
                Logger.LogDebug(
                    "Get from {baseUrl} was {statusCode} (took:{duration}ms)",
                    Config.BaseUrl,
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

        public void FinalizeImport<TKey>(ICommandProcessorOptions<TKey> options)
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

            var json = Serializer.Serialize(
                new ImportBatchStatus
                {
                    From = options.From,
                    Until = options.Until,
                    Completed = importCompleted
                });

            Using(client =>
            {
                Logger.LogDebug("Posting batch status from {baseUrl}", Config.BaseUrl);
                var watch = Stopwatch.StartNew();
                var response = client
                    .PostAsync(
                        Config.ImportBatchStatusEndpoint,
                        new StringContent(json, Encoding.UTF8, "application/json")
                    )
                    .GetAwaiter()
                    .GetResult();
                watch.Stop();
                Logger.LogDebug(
                    "Post from {baseUrl} was {statusCode} (took:{duration}ms)",
                    Config.BaseUrl,
                    response.StatusCode,
                    watch.ElapsedMilliseconds);

                response.EnsureSuccessStatusCode();
            });
        }

        protected class MediaTypeNames
        {
            public class Application
            {
                // placeholder for System.Net.Mime.MediaTypeNames.Application.Json
                // field is not available in .Net Standard 2.0
                public const string Json = "application/json";
            }
        }
    }
}
