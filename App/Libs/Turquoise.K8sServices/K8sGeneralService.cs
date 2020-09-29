using AutoMapper;
using k8s;
using Microsoft.Extensions.Logging;
using Turquoise.K8s.K8sClients;
using Turquoise.K8sServices.K8sClients;

namespace Turquoise.K8sServices
{
    public class K8sGeneralService
    {
        public Kubernetes Client { get; }
        private IMapper mapper;
        public K8sDeploymentClient DeploymentClient { get; }
        public K8sEventClient EventClient { get; }
        public K8sIngressClient IngressClient { get; }
        public K8sMetricsClient MetricsClient { get; }
        public K8sNamespaceClient NamespaceClient { get; }
        public K8sNodeClient NodeClient { get; }
        public K8sPodClient PodClient { get; set; }
        public IstioVirtualServiceClient VirtualServiceClient { get; }
        public K8sServiceClient ServiceClient { get; }
        private ILogger<K8sGeneralService> logger;



        public K8sGeneralService(IKubernetesClient kubernetesClient, IMapper mapper, ILogger<K8sGeneralService> logger)
        {
            this.Client = kubernetesClient.Client;
            this.mapper = mapper;
            this.logger = logger;

            DeploymentClient = new K8sDeploymentClient(Client, logger, mapper);
            EventClient = new K8sEventClient(Client, logger, mapper);
            IngressClient = new K8sIngressClient(Client, logger, mapper);
            MetricsClient = new K8sMetricsClient(Client, logger, mapper);
            NamespaceClient = new K8sNamespaceClient(Client, logger, mapper);
            NodeClient = new K8sNodeClient(Client, logger, mapper);
            PodClient = new K8sPodClient(Client, logger, mapper);
            VirtualServiceClient = new IstioVirtualServiceClient(Client, logger, mapper);
            ServiceClient = new K8sServiceClient(Client, IngressClient, VirtualServiceClient, logger, mapper);
        }
    }
}