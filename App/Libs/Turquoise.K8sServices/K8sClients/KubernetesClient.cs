using k8s;

namespace Turquoise.K8sServices.K8sClients
{

    public interface IKubernetesClient
    {
        Kubernetes Client { get; }
    }
    public class KubernetesClientFromConfigFile : IKubernetesClient
    {
        public Kubernetes Client { get; }
        public KubernetesClientFromConfigFile()
        {
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();
            Client = new Kubernetes(config);
        }
    }


    public class KubernetesClientInClusterConfig : IKubernetesClient
    {
        public Kubernetes Client { get; }
        public KubernetesClientInClusterConfig()
        {
            var config = KubernetesClientConfiguration.InClusterConfig();
            Client = new Kubernetes(config);
        }
    }
}