using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Turquoise.Api.HealthMonitoring.GRPCServices
{
    // [Authorize]
    public class CountryGRPCService : CountryService.CountryServiceBase
    {
        private ILogger<CountryGRPCService> _logger;

        public CountryGRPCService(ILogger<CountryGRPCService> logger)
        {
            _logger = logger;
        }
        public override Task<CountriesReply> GetAll(EmptyRequest request, Grpc.Core.ServerCallContext context)
        {
            _logger.LogCritical("Got in to GetAll GRPC");
            var countriesReply = new CountriesReply();
            countriesReply.Countries.Add(new CountryReply { Description = "Blah", Id = 1, Name = "Australia" });
            countriesReply.Countries.Add(new CountryReply { Description = "Blah Blahhh", Id = 2, Name = "Turkey" });
            return Task.FromResult(countriesReply);
        }

    }
}