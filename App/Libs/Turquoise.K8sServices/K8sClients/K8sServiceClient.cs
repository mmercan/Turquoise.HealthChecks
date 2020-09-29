using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;
using Turquoise.K8sServices.K8sClients;
using Turquoise.Models.Mongo;

namespace Turquoise.K8s.K8sClients
{
    public class K8sServiceClient
    {
        private Kubernetes client;
        private K8sIngressClient ingressClient;
        private IstioVirtualServiceClient virtualServiceClient;
        private ILogger logger;
        private IMapper mapper;

        public K8sServiceClient(Kubernetes kubernetesClient, K8sIngressClient ingressClient, IstioVirtualServiceClient virtualServiceClient, ILogger logger, IMapper mapper)
        {
            this.client = kubernetesClient;
            this.ingressClient = ingressClient;
            this.virtualServiceClient = virtualServiceClient;
            this.logger = logger;
            this.mapper = mapper;

        }

        public IList<V1Service> GetAll()
        {
            return client.ListServiceForAllNamespaces().Items;

        }

        public async Task<IList<V1Service>> GetAllAsync()
        {
            var services = await client.ListServiceForAllNamespacesAsync();
            return services.Items;
        }

        public IList<V1Service> Get(string namespaceparam)
        {
            return client.ListNamespacedService(namespaceparam).Items;

        }

        public async Task<IList<V1Service>> GetAsync(string namespaceparam)
        {
            var services = await client.ListNamespacedServiceAsync(namespaceparam);
            return services.Items;
        }


        public Task<List<ServiceV1>> GetAllMongoServiceAsync()
        {
            var returnservices = new List<Turquoise.Models.Mongo.ServiceV1>();
            var ingressTask = this.ingressClient.GetAllAsync();
            var serviceTask = this.GetAllAsync();
            var vstask = virtualServiceClient.GetAllAsync();
            Task.WaitAll(ingressTask, serviceTask, vstask);
            var ingresses = ingressTask.Result;
            var services = serviceTask.Result;
            var virtualservices = vstask.Result;

            foreach (var service in services)
            {
                var serviceName = service.Name();
                var serviceNamespace = service.Namespace();

                var dtoitems = mapper.Map<Turquoise.Models.Mongo.ServiceV1>(service);

                if (dtoitems.Annotations != null && dtoitems.Annotations.Count > 0 && dtoitems.Annotations.Any(p => p.Key == "healthcheck/crontab"))
                {
                    var crontab = dtoitems.Annotations.FirstOrDefault(p => p.Key == "healthcheck/crontab");
                    if (crontab != null && crontab.Value != null)
                    {
                        dtoitems.CronTab = crontab.Value;
                        try
                        {
                            var Schedule = CronExpressionDescriptor.ExpressionDescriptor.GetDescription(crontab.Value);
                            dtoitems.CronDescription = Schedule;
                            logger.LogWarning("CronTab Found " + Schedule.ToString());
                        }
                        catch (Exception ex)
                        {
                            dtoitems.CronTabException = ex.Message;
                        }
                    }
                }
                returnservices.Add(dtoitems);

                foreach (var ing in ingresses.Where(p => p.Namespace() == serviceNamespace))
                {
                    var paths = ing.Spec.Rules.FirstOrDefault(p => p.Http.Paths.All(pp => pp.Backend.ServiceName == serviceName));
                    if (paths != null)
                    {
                        var prefix = "http://";
                        if (ing.Spec.Tls != null)
                        {
                            prefix = "https://";
                        }
                        dtoitems.IngressUrl = prefix + paths.Host;
                    }
                }

                var vs = virtualservices.FirstOrDefault(p => p.Namespace == serviceNamespace && p.Service == serviceName);
                if (vs != null)
                {
                    dtoitems.VirtualServiceUrl = "http://" + vs.Host;
                }
            }

            return Task.FromResult(returnservices);

        }

    }
}