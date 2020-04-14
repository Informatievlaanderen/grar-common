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
    using Crab;
    using Messages;

    public class HttpApiProxy : HttpApiProxyBase
    {
        public HttpApiProxy(ILogger logger,
            JsonSerializer serializer,
            IHttpApiProxyConfig config,
            ImportFeed importFeed)
            : base(logger, serializer, config, importFeed)
        { }

        public override void ImportBatch<TKey>(IEnumerable<KeyImport<TKey>> imports)
        {
            var json = Serializer.Serialize(imports.Select(i => i.Commands));
            using (var client = CreateImportClient())
            {
                Logger.LogDebug("Posting to {baseUrl}", client.BaseAddress);
                Logger.LogTrace($"Payload:{Environment.NewLine}{{json}}", json);

                var watch = Stopwatch.StartNew();

                var response = client
                    .PostAsync(
                        Config.ImportEndpoint,
                        CreateJsonContent(json))
                    .GetAwaiter()
                    .GetResult();

                watch.Stop();

                Logger.LogDebug(
                    "Post to {baseUrl} was {statusCode} (took:{duration}ms)",
                    client.BaseAddress,
                    response.StatusCode,
                    watch.ElapsedMilliseconds);

                response.EnsureSuccessStatusCode();
            }
        }
    }

    public abstract class HttpApiProxyBase : IApiProxy
    {
        protected readonly IHttpApiProxyConfig Config;
        private readonly ImportFeed _importFeed;
        protected readonly ILogger Logger;
        protected readonly JsonSerializer Serializer;

        protected HttpApiProxyBase(
            ILogger logger,
            JsonSerializer serializer,
            IHttpApiProxyConfig config,
            ImportFeed importFeed)
        {
            Serializer = serializer;
            Logger = logger;
            Config = config;
            _importFeed = importFeed;
        }

        protected HttpClient CreateImportClient()
        {
            var client = new HttpClient
            {
                BaseAddress = Config.BaseUrl,
                Timeout = TimeSpan.FromMinutes(Config.HttpTimeoutMinutes)
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));

            if (!string.IsNullOrEmpty(Config.AuthUserName) && !string.IsNullOrEmpty(Config.AuthPassword))
            {
                var encodedString = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Config.AuthUserName}:{Config.AuthPassword}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedString);
            }

            return client;
        }

        public abstract void ImportBatch<TKey>(IEnumerable<KeyImport<TKey>> imports);

        protected static StringContent CreateJsonContent(string jsonValue)
            => new StringContent(jsonValue, Encoding.UTF8, MediaTypeNames.Application.Json);

        public ICommandProcessorOptions<TKey> GetImportOptions<TKey>(
            ImportOptions options,
            ICommandProcessorBatchConfiguration<TKey> configuration)
        {
            ICommandProcessorOptions<TKey> processorOptions;
            using (var client = CreateImportClient())
            {
                var requestUri = $"{Config.ImportBatchStatusEndpoint}/{_importFeed.Name}";
                Logger.LogDebug(
                    "Getting batch status from {baseUrl}/{requestUri}",
                    client.BaseAddress,
                    requestUri);

                var watch = Stopwatch.StartNew();

                var response = client
                    .GetAsync(requestUri)
                    .GetAwaiter()
                    .GetResult();

                watch.Stop();

                Logger.LogDebug(
                    "Get from {baseUrl}/{requestUri} was {statusCode} (took:{duration}ms)",
                    client.BaseAddress,
                    requestUri,
                    response.StatusCode,
                    watch.ElapsedMilliseconds);

                var content = response
                    .EnsureSuccessStatusCode()
                    .Content
                    .ReadAsStringAsync()
                    .GetAwaiter()
                    .GetResult() ?? string.Empty;

                var batchStatus = JsonConvert.DeserializeObject<BatchStatus>(content);
                processorOptions = options.CreateProcessorOptions(batchStatus, configuration);
            }

            return processorOptions;
        }

        public void InitializeImport<TKey>(ICommandProcessorOptions<TKey> options) =>
            PostImportBatchStatus(options, Batch.Start);

        public void FinalizeImport<TKey>(ICommandProcessorOptions<TKey> options)
            => PostImportBatchStatus(options, Batch.Completed);

        private void PostImportBatchStatus<TKey>(ICommandProcessorOptions<TKey> options, bool importCompleted)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            var json = Serializer.Serialize(
                new BatchStatusUpdate
                {
                    ImportFeed = _importFeed,
                    From = options.From.ToDateTimeOffset(),
                    Until = options.Until.ToDateTimeOffset(),
                    CrabTimeScope = $"{options.From.ToCrabDateTime()} - {options.Until.ToCrabDateTime()}",
                    Completed = importCompleted
                });

            using (var client = CreateImportClient())
            {
                var requestUri = Config.ImportBatchStatusEndpoint;
                Logger.LogDebug(
                    "Posting batch status from {baseUrl}/{requestUri}",
                    client.BaseAddress,
                    requestUri);

                var watch = Stopwatch.StartNew();

                var response = client
                    .PostAsync(
                        requestUri,
                        CreateJsonContent(json))
                    .GetAwaiter()
                    .GetResult();

                watch.Stop();

                Logger.LogDebug(
                    "Post to {baseUrl}/{requestUri} was {statusCode} (took:{duration}ms)",
                    client.BaseAddress,
                    requestUri,
                    response.StatusCode,
                    watch.ElapsedMilliseconds);

                response.EnsureSuccessStatusCode();
            }
        }

        private static class Batch
        {
            public static bool Start => false;
            public static bool Completed => true;
        }

        private static class MediaTypeNames
        {
            public static class Application
            {
                // placeholder for System.Net.Mime.MediaTypeNames.Application.Json
                // field is not available in .Net Standard 2.0
                public const string Json = "application/json";
            }
        }
    }
}
