using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Turquoise.K8s.Services
{
    public class AZAuthService
    {
        private ILogger<AZAuthService> logger;
        private IOptions<AZAuthServiceSettings> settingsOptions;

        // private static readonly HttpClient client = new HttpClient();
        public AZAuthService(ILogger<AZAuthService> logger, IOptions<AZAuthServiceSettings> settingsOptions)
        {
            this.logger = logger;
            this.settingsOptions = settingsOptions;
        }

        public async Task<string> Authenticate()
        {

            if (settingsOptions.Value == null)
            {
                throw new ArgumentNullException("settingsOptions");
            }
            var setting = settingsOptions.Value;

            if (string.IsNullOrEmpty(setting.Secret))
            {
                throw new ArgumentNullException("setting.Secret");
            }
            logger.LogCritical(setting.Secret.Length + " Chars on Secret");

            var url = "https://login.microsoftonline.com/" + setting.TenantId + "/oauth2/token?resource=" + setting.ClientId;

            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            nvc.Add(new KeyValuePair<string, string>("client_id", setting.ClientId));
            nvc.Add(new KeyValuePair<string, string>("client_secret", setting.Secret));
            nvc.Add(new KeyValuePair<string, string>("resource", setting.ClientId));
            var client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(nvc) };

            var res = await client.SendAsync(req);

            logger.LogCritical("Az Auth Status Code " + res.StatusCode.ToString());

            var result = res.Content.ReadAsStringAsync();
            result.Wait();
            logger.LogCritical("Az Result is");
            logger.LogCritical(result.Result);
            var s = Newtonsoft.Json.JsonConvert.DeserializeObject<AZToken>(result.Result);
            var token = s.AccessToken as string;

            logger.LogCritical(token);


            var expires_on = s.ExpiresOn as string;
            var date = convertDatetime(expires_on);
            logger.LogCritical("Token expires on : " + date.ToString());
            return token;
        }

        private DateTime convertDatetime(string unixdate)
        {
            int unixdatenumber = 0;
            if (Int32.TryParse(unixdate, result: out unixdatenumber))
            {
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(unixdatenumber).ToLocalTime();
                return dtDateTime;
            }
            else
            {
                return DateTime.Now;
            }
        }
    }

    public class AZAuthServiceSettings
    {
        public string ClientId { get; set; }
        public string TenantId { get; set; }
        public string Secret { get; set; }
    }
    public class AZToken
    {
        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }


        [JsonProperty(PropertyName = "expires_in")]
        public string ExpiresIn { get; set; }


        [JsonProperty(PropertyName = "expires_on")]
        public string ExpiresOn { get; set; }


        [JsonProperty(PropertyName = "not_before")]
        public string NotBefore { get; set; }


        [JsonProperty(PropertyName = "resource")]
        public string Resource { get; set; }



        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
    }



}