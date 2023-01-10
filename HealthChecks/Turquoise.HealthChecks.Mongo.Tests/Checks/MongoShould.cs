using System;
using System.Threading.Tasks;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Xunit;

namespace Turquoise.HealthChecks.Mongo.Tests.Checks
{
    public class MongoShould
    {

        string connectionString = "mongodb://root:hbMnztmZ4w9JJTGZ@localhost:27017/admin?readPreference=primary";
        string failedConnectionString = "mongodb://root:hbMnztmZ4w9JJTGZ@mongooo/admin?readPreference=primary";
        HealthCheckContext context = new HealthCheckContext();



        private readonly TestcontainersContainer _mongoContainer = new TestcontainersBuilder<TestcontainersContainer>()
            .WithImage("mongo:latest")
            .WithEnvironment("MONGO_INITDB_ROOT_USERNAME", "root")
            .WithEnvironment("MONGO_INITDB_ROOT_PASSWORD", "hbMnztmZ4w9JJTGZ")
            .WithEnvironment("MONGO_INITDB_DATABASE", "admin")
            .WithPortBinding(27017, 27017)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(27017))
            .Build();



        //private readonly TestcontainersContainer _mongoContainer =  new TestcontainersBuilder<MongoDbTestcontainer>().WithDatabase(new MongoDbTestcontainerConfiguration
        //{
        //    Database="admin",
        //    Username= "root",
        //    Password= "hbMnztmZ4w9JJTGZ",
        //})
        //   .WithImage("mongo:latest")
        //   .WithPortBinding(27017, 27017)
        //   .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(27017))
        //   .Build();


        [Fact]
        public void CreateaMongoInstance()
        {
            var check = new MongoHealthCheck(connectionString);
        }

        [Fact]
        public async Task RunMongoHealthCheck()
        {
            await _mongoContainer.StartAsync();
            var check = new MongoHealthCheck(connectionString);
            var result = await check.CheckHealthAsync(context);
            Assert.Equal(HealthStatus.Healthy, result.Status);
            await _mongoContainer.StopAsync();
        }


        [Fact]
        public async Task RunMongoHealthCheckWithWrongConString()
        {
            var check = new MongoHealthCheck(failedConnectionString);
            var result = await check.CheckHealthAsync(context);
            Assert.Equal(HealthStatus.Unhealthy, result.Status);
        }

        [Fact]
        public void AddtothePipelineWorks()
        {
            var services1 = new ServiceCollection()
            .AddLogging();
            services1.AddHealthChecks().AddMongoHealthCheck(connectionString);
            var serviceProvider = services1.BuildServiceProvider();
            //  var healthCheckService = serviceProvider.GetService<HealthCheckService>();
            // var result = await healthCheckService.CheckHealthAsync();
            // Assert.Equal(HealthStatus.Healthy, result.Status);

        }
    }
}
