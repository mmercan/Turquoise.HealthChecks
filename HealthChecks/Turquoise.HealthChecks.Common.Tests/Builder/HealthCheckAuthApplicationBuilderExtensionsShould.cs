
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Turquoise.HealthChecks.Common.Checks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Turquoise.HealthChecks.Common.Tests.Builder
{
    public class HealthCheckAuthApplicationBuilderExtensionsShould
    {
        [Fact]
        public void AddtothePipelineWorks()
        {
            var host = WebHost.CreateDefaultBuilder()
             .ConfigureServices((cs) =>
             {
                 cs.AddLogging();
                 cs.AddHealthChecks()
                 .AddSystemInfoCheck();

                 cs.AddSingleton<HealthCheckMiddlewareAuth>();
             })
             .Configure((app) =>
             {
                 app.UseHealthChecksWithAuth("/isaliveandwell")
                 .UseHealthChecksWithAuth("/isaliveandwell", new HealthCheckOptions())
                 .UseHealthChecksWithAuth("/isaliveandwell", "4444")
                 .UseHealthChecksWithAuth("/isaliveandwell", 4445)
                 .UseHealthChecksWithAuth("/isaliveandwell", "4444", new HealthCheckOptions())
                 .UseHealthChecksWithAuth("/isaliveandwell", 4446, new HealthCheckOptions());

             }).Build();

            using (host)
            {
                var hoststart = host.StartAsync();
                hoststart.Wait();
                // // Monitor for new background queue work items
                var moqContext = new Mock<HttpContext>();
                var httpContext = moqContext.Object;

                // var healthCheckMiddlewareAuth = host.Services.GetService<HealthCheckMiddlewareAuth>();
                // var task = healthCheckMiddlewareAuth.InvokeAsync(httpContext);
                // task.Wait();
                // // monitorLoop.StartMonitorLoop();
                // var waitforshutdown = host.WaitForShutdownAsync();
                // waitforshutdown.Wait();
            }


        }
    }
}