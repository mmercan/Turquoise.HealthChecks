using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Turquoise.K8s.Services;
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


        public async Task<List<string>> DownloadAsync(ServiceV1 service)
        {
            var results = new List<string>();

            if (checkAuthentication(service))
            {
                logger.LogInformation("Auth is Started");
                string bearerToken = await azAuthService.Authenticate();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            }

            var endpoints = extractUrlFromService(service);
            string isAliveAndWellSuffix = getIsAliveAndWellSuffix(service);
            isAliveAndWellSuffix = "/Health/IsAliveAndWell";
            if (endpoints.Count > 0)
            {
                foreach (string url in endpoints)
                {
                    Uri baseUrl = new Uri(url);
                    Uri isaliveandwellUri = new Uri(baseUrl, isAliveAndWellSuffix);
                    var result = await DownloadAsync(isaliveandwellUri);
                    results.Add(result);
                }
            }
            else
            {
                throw new ArgumentException("End Point Not Found For Service" + service.Name);
            }
            return results;
        }

        public async Task<string> DownloadAsync(Uri url)
        {
            var getitem = await client.GetAsync(url);

            if (!getitem.IsSuccessStatusCode)
            {
                logger.LogCritical(getitem.StatusCode.ToString() + " ");
                throw new HttpRequestException("Failed");
            }

            var content = await getitem.Content.ReadAsStringAsync();
            return content;

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
                endpoints.AddRange(service.InternalEndpoints);
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
}