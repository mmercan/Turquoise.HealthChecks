using Microsoft.Extensions.DependencyInjection;
using Turquoise.Common.Scheduler.Deployment;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DeploymentSchedulerRepositoryExtension
    {
        public static IServiceCollection AddDeploymentSchedulerRepository<T>(this IServiceCollection serviceCollection) where T : new()
        {
            serviceCollection.AddSingleton<DeploymentSchedulerScaleUpRepository<T>>();
            serviceCollection.AddSingleton<DeploymentSchedulerScaleDownRepository<T>>();
            return serviceCollection;
        }

    }
}
