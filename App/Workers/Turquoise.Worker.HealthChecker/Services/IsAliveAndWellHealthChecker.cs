using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Turquoise.Common.Services;
using Turquoise.Models.Mongo;

namespace Turquoise.HealthChecker.Services
{
    public class IsAliveAndWellHealthChecker
    {

        HttpClient client;
        private ILogger<IsAliveAndWellHealthChecker> logger;
        private IConfiguration configuration;
        private AZAuthService azAuthService;

        public IsAliveAndWellHealthChecker(HttpClient client, ILogger<IsAliveAndWellHealthChecker> logger, IConfiguration configuration, AZAuthService azAuthService)
        {
            this.client = client;
            this.logger = logger;
            this.configuration = configuration;
            this.azAuthService = azAuthService;
        }

        public async Task<List<IsAliveAndWellResult>> DownloadAsync(ServiceV1 service)
        {
            var results = new List<IsAliveAndWellResult>();

            await authenticate(false, service);

            var endpoints = extractUrlFromService(service);
            string isAliveAndWellSuffix = getIsAliveAndWellSuffix(service);
            // isAliveAndWellSuffix = "/Health/IsAliveAndWell";
            if (endpoints.Count > 0)
            {
                foreach (string url in endpoints)
                {
                    Uri baseUrl = new Uri(url);
                    Uri isaliveandwellUri = new Uri(baseUrl, isAliveAndWellSuffix);

                    logger.LogInformation("Cheking " + isaliveandwellUri.ToString());
                    var result = await DownloadAsync(isaliveandwellUri);

                    results.Add(result);
                }
            }
            else
            {
                throw new ArgumentException("End Point Not Found For Service" + service.Name);
            }
            logger.LogInformation("returning DownloadAsync : " + service.NameandNamespace);
            return results;
        }

        public async Task<IsAliveAndWellResult> DownloadAsync(Uri url)
        {

            logger.LogCritical("Uri is : " + url.ToString());
            var getitem = await client.GetAsync(url);

            logger.LogCritical("GetAsync Completed " + url.ToString() + " with " + getitem.StatusCode.ToString());
            var isSuccessStatusCode = true;
            if (!getitem.IsSuccessStatusCode)
            {
                logger.LogCritical(getitem.StatusCode.ToString() + " ");
                isSuccessStatusCode = false;
                // throw new HttpRequestException("Failed");
            }
            else if (getitem.StatusCode == HttpStatusCode.Unauthorized)
            {

            }
            var status = getitem.StatusCode.ToString();
            var content = await getitem.Content.ReadAsStringAsync();
            return new IsAliveAndWellResult { Result = content, Status = status, IsSuccessStatusCode = isSuccessStatusCode, CheckedUrl = url.AbsoluteUri };

        }

        private async Task authenticate(bool force, ServiceV1 service)
        {

            if (checkAuthentication(service))
            {
                logger.LogInformation("Auth is Started");
                string bearerToken = await azAuthService.Authenticate();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            }
        }

        private string getIsAliveAndWellSuffix(ServiceV1 service)
        {
            return service.Annotations.FirstOrDefault(p => p.Key == "healthcheck/isaliveandwell")?.Value;
        }
        private List<string> extractUrlFromService(ServiceV1 service)
        {
            List<string> endpoints = new List<string>();
            if (configuration["RunOnCluster"] == "true")
            {
                foreach (var item in service.InternalEndpoints)
                {
                    endpoints.Add("http://" + item);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(service.VirtualServiceUrl))
                {
                    endpoints.Add(service.VirtualServiceUrl);
                }
                else if (!string.IsNullOrWhiteSpace(service.IngressUrl))
                {
                    endpoints.Add(service.IngressUrl);
                }
                else if (service.ExternalEndpoints != null && service.ExternalEndpoints.Count > 0)
                {
                    endpoints.AddRange(service.ExternalEndpoints);
                }
                else
                {
                    endpoints.AddRange(service.InternalEndpoints);
                }
            }
            return endpoints;
        }

        private bool checkAuthentication(ServiceV1 service)
        {
            return service.Annotations.Any(p => p.Key == "healthcheck/clientid");
        }

    }
    public class IsAliveAndWellResult
    {
        public string Result { get; set; }
        public string Status { get; set; }
        public bool IsSuccessStatusCode { get; set; }
        public string CheckedUrl { get; set; }
    }
}