using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DeploymentSchedulerRepositoryExtension
    {
        public static IServiceCollection AddDeploymentSchedulerRepository<T>(this IServiceCollection serviceCollection) where T : new()
        {
            // serviceCollection.Configure<MangoBaseRepoSettings<T>>(o => o = options);
            serviceCollection.AddSingleton<Turquoise.Common.Scheduler.DeploymentSchedulerScaleUpRepository<T>>();
            serviceCollection.AddSingleton<Turquoise.Common.Scheduler.DeploymentSchedulerScaleDownRepository<T>>();
            return serviceCollection;
        }


        //     public static IServiceCollection AddHealthCheckSchedulerRepository<T>(
        //         this IServiceCollection serviceCollection,
        //         IConfiguration options) where T : new()
        //     {
        //         serviceCollection.Configure<MangoBaseRepoSettings<T>>(options);
        //         serviceCollection.AddSingleton<MangoBaseRepo<T>>();
        //         return serviceCollection;
        //     }
        // }
    }
}
