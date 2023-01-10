using System;
using System.Threading.Tasks;
using Turquoise.HealthChecks.Network.Ping;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Xunit;

namespace Turquoise.HealthChecks.Network.Tests.Checks
{
    public class PingHelperShould
    {
        string connectionString = "www.google.com:443";
        HealthCheckContext context = new HealthCheckContext();

        [Fact]
        public void CreateaPingInstance()
        {
            PingHelper check = new PingHelper();
        }

        [Fact]
        public void RunPingHealthCheck()
        {
            var helper = new PingHelper();
            helper.TcpPing(connectionString);

            Assert.Throws<Exception>(() => { helper.TcpPing("blah"); });

        }
    }
}
