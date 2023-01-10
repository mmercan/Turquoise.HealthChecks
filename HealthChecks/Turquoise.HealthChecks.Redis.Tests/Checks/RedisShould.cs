using System;
using System.Threading.Tasks;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Xunit;

namespace Turquoise.HealthChecks.Redis.Tests.Checks
{
    public class RedisShould
    {

        string connectionString = "127.0.0.1:6379,defaultDatabase=2,password=yourpassword";


        // private readonly TestcontainersContainer _redisContainer = new TestcontainersBuilder<TestcontainersContainer>()
        //     .WithImage("redis:latest")
        //     .WithEnvironment("MONGO_INITDB_ROOT_USERNAME", "root")
        //     .WithEnvironment("MONGO_INITDB_ROOT_PASSWORD", "hbMnztmZ4w9JJTGZ")
        //     .WithEnvironment("MONGO_INITDB_DATABASE", "admin")
        //     .WithPortBinding(27017, 27017)
        //     .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(27017))
        //     .Build();



        private readonly TestcontainersContainer _redisContainer = new TestcontainersBuilder<RedisTestcontainer>().WithDatabase(new RedisTestcontainerConfiguration
        {
            // Password = "yourpassword",
        })
          .WithImage("redis:latest")
          .WithPortBinding(6379, 6379)
          .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(6379))
          .Build();



        public RedisShould()
        {

        }

        HealthCheckContext context = new HealthCheckContext();
        [Fact]
        public async Task CreateaRedisInstance()
        {
            await _redisContainer.StartAsync();
            RedisHealthCheck check = new RedisHealthCheck(connectionString);
            await _redisContainer.StopAsync();
        }



        [Fact]
        public async Task RunRedisHealthCheck()
        {
            await _redisContainer.StartAsync();
            var check = new RedisHealthCheck(connectionString);
            var result = await check.CheckHealthAsync(context);
            Assert.Equal(HealthStatus.Healthy, result.Status);
            await _redisContainer.StopAsync();
        }


        [Fact]
        public async Task AddtothePipelineWorks()
        {
            await _redisContainer.StartAsync();
            var services1 = new ServiceCollection()
            .AddLogging();
            services1.AddHealthChecks().AddRedisHealthCheck(connectionString);
            var serviceProvider = services1.BuildServiceProvider();
            //  var healthCheckService = serviceProvider.GetService<HealthCheckService>();
            // var result = await healthCheckService.CheckHealthAsync();
            // Assert.Equal(HealthStatus.Healthy, result.Status);

            await _redisContainer.StopAsync();

        }

    }
}
