using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Turquoise.Api.HealthMonitoring.GRPCServices
{
    public class GreeterGRPCService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterGRPCService> _logger;

        public GreeterGRPCService(ILogger<GreeterGRPCService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request,
            ServerCallContext context)
        {
            _logger.LogInformation("Saying hello to {Name}", request.Name);
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}