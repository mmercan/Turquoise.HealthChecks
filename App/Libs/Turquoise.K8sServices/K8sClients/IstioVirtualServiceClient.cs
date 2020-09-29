using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using k8s;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Turquoise.Models.Mongo;

namespace Turquoise.K8sServices.K8sClients
{
    public class IstioVirtualServiceClient : IK8sClient<VirtualServiceV1>
    {
        private readonly Kubernetes client;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public IstioVirtualServiceClient(Kubernetes kubernetesClient, ILogger logger, IMapper mapper)
        {
            this.client = kubernetesClient;
            this.logger = logger;
            this.mapper = mapper;
        }



        public async Task<IList<VirtualServiceV1>> GetAllAsync()
        {
            List<VirtualServiceV1> items = new List<VirtualServiceV1>();
            var result = await client.ListNamespacedCustomObjectAsync("networking.istio.io", "v1alpha3", "", "virtualservices") as JObject;
            var jtokens = result.GetValue("items").AsJEnumerable();
            foreach (JObject jitem in jtokens)
            {
                var host = jitem.SelectToken("spec.hosts[0]").ToString();
                var service = jitem.SelectToken("spec.http[0].route[0].destination.host").ToString();
                var port = jitem.SelectToken("spec.http[0].route[0].destination.port.number").ToString();

                var name = jitem.SelectToken("metadata.name").ToString();
                var namespaceparam = jitem.SelectToken("metadata.namespace").ToString();

                logger.LogCritical(host + " > " + service + ":" + port);
                var item = new VirtualServiceV1 { Host = host, Service = service, Port = port, Name = name, Namespace = namespaceparam };
                items.Add(item);
            }
            return items;
        }

        public async Task<IList<VirtualServiceV1>> GetAsync(string nameSpace)
        {
            List<VirtualServiceV1> items = new List<VirtualServiceV1>();
            var result = await client.ListNamespacedCustomObjectAsync("networking.istio.io", "v1alpha3", nameSpace, "virtualservices") as JObject;
            var jtokens = result.GetValue("items").AsJEnumerable();
            foreach (JObject jitem in jtokens)
            {
                var host = jitem.SelectToken("spec.hosts[0]").ToString();
                var service = jitem.SelectToken("spec.http[0].route[0].destination.host").ToString();
                var port = jitem.SelectToken("spec.http[0].route[0].destination.port.number").ToString();

                var name = jitem.SelectToken("metadata.name").ToString();
                var namespaceparam = jitem.SelectToken("metadata.namespace").ToString();

                logger.LogCritical(host + " > " + service + ":" + port);
                var item = new VirtualServiceV1 { Host = host, Service = service, Port = port, Name = name, Namespace = nameSpace };
                items.Add(item);
            }
            return items;
        }

        public IList<VirtualServiceV1> Get(string nameSpace)
        {
            List<VirtualServiceV1> items = new List<VirtualServiceV1>();
            var result = client.ListNamespacedCustomObject("networking.istio.io", "v1alpha3", nameSpace, "virtualservices") as JObject;
            var jtokens = result.GetValue("items").AsJEnumerable();
            foreach (JObject jitem in jtokens)
            {
                var host = jitem.SelectToken("spec.hosts[0]").ToString();
                var service = jitem.SelectToken("spec.http[0].route[0].destination.host").ToString();
                var port = jitem.SelectToken("spec.http[0].route[0].destination.port.number").ToString();

                var name = jitem.SelectToken("metadata.name").ToString();
                var namespaceparam = jitem.SelectToken("metadata.namespace").ToString();

                logger.LogCritical(host + " > " + service + ":" + port);
                var item = new VirtualServiceV1 { Host = host, Service = service, Port = port, Name = name, Namespace = nameSpace };
                items.Add(item);
            }
            return items;
        }

    }
}