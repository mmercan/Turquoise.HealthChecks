using System;
using System.Threading.Tasks;
using Turquoise.HealthChecks.Common.Checks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Builders;
using System.IO;
using DotNet.Testcontainers.Configurations;

namespace Turquoise.HealthChecks.Common.Tests.Checks
{
    public class SqlConnectionHealthCheckTests
    {


        private readonly ITestOutputHelper output;
        string conection = "Server=localhost;Database=master;User Id=sa;Password=MySentP@ssw0rd;TrustServerCertificate=True";  //Environment.GetEnvironmentVariable("SentinelConnection");





        public SqlConnectionHealthCheckTests(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task ConnectDatabase()
        {

            IOutputConsumer outputConsumer = Consume.RedirectStdoutAndStderrToStream(new MemoryStream(), new MemoryStream());

            TestcontainersContainer _sqlContainer = new TestcontainersBuilder<MsSqlTestcontainer>()
             .WithDatabase(new MsSqlTestcontainerConfiguration
             {
                 Password = "MySentP@ssw0rd",
                 Database = "sentinel",
                 //Username="sa",
                 Port = 1433,

             })
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithCleanUp(true)
            //.Build();
            // .WithImage("mcr.microsoft.com/mssql/server:2019-latest")
            // .WithEnvironment("ACCEPT_EULA", "true")
            // .WithEnvironment("SA_PASSWORD", "MySentP@ssw0rd")
            //.WithBindMount(Path.GetFullPath("sql"), "/scripts/")
            //.WithCommand("/bin/bash", "-c", "/scripts/init.sh")

            .WithPortBinding(1433, 1433)
            //  .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433))
            //.WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged(outputConsumer.Stdout, "INIT_COMPLETE"))
             .Build();




            await _sqlContainer.StartAsync();

            HealthCheckContext context = new HealthCheckContext();

            SqlConnectionHealthCheck sql = new SqlConnectionHealthCheck(conection);
            var result = await sql.CheckHealthAsync(context);
            Assert.Equal(HealthStatus.Healthy, result.Status);

            // task.Wait();
            // // 
            // output.WriteLine("Description : " + task.Result.Description);

            // foreach (var item in task.Result.Data)
            // {
            //     output.WriteLine("Data  " + item.Key + ":" + item.Value.ToString());
            // }
            // output.WriteLine("Data Counts : " + task.Result.Data.Count);
            // Assert.Equal(HealthStatus.Healthy, task.Result.Status);

        }

        [Fact]
        public async Task AddMiddlewareAsync()
        {
            var services = new ServiceCollection()
            .AddLogging();

            services.AddHealthChecks()
            .SqlConnectionHealthCheck(conection)
            .SqlConnectionHealthCheck(conection, "select 1");


            var serviceProvider = services.BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<DIHealthCheck>();
            var healthChecksBuilder = serviceProvider.GetService<IHealthChecksBuilder>();
            var healthCheckService = serviceProvider.GetService<HealthCheckService>();
            var resultTask = healthCheckService.CheckHealthAsync();

            await Task.Run(() =>
            {

            });

        }


    }
}