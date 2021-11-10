using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Turquoise.HealthChecks.Common.Checks
{
    public static partial class HealthCheckBuilderExtensions
    {
        public static IHealthChecksBuilder AddConfigurationChecker(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            return builder.AddTypeActivatedCheck<IConfigurationBaseHealthCheck>($"IConfiguration", null, null, configuration);
        }


    }

    public class IConfigurationBaseHealthCheck : IHealthCheck
    {
        readonly ILogger<IConfigurationBaseHealthCheck> _logger;
        private readonly IConfiguration _configuration;
        public IConfigurationBaseHealthCheck(ILogger<IConfigurationBaseHealthCheck> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }


        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Task.Run(() =>
            {
                try
                {
                    var count = _configuration.AsEnumerable().Count().ToString();
                    string description = "Configuration is succeesful with response  items: " + count;

                    var data = new Dictionary<string, object>();
                    foreach (var item in _configuration.AsEnumerable())
                    {
                        data.Add(item.Key, item.Value);
                    }

                    return HealthCheckResult.Healthy(description, data);
                }
                catch (Exception ex)
                {
                    string description = HandleExceptionMessage(ex);
                    IReadOnlyDictionary<string, object> data = new Dictionary<string, object> {
                        { "type", "IConfigurationChecker" },
                        { "Exception", "failed with exception " + description },
                    };
                    return HealthCheckResult.Unhealthy(description, null, data);
                }
            });
        }
        public string HandleExceptionMessage(Exception ex)
        {
            var Message = ex.InnerException?.InnerException?.Message;
            if (Message == null) { Message = ex.InnerException?.Message; }
            if (Message == null) { Message = ex.Message; }
            return Message;
        }
    }


}
