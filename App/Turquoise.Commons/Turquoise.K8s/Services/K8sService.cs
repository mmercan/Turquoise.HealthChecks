using System.Collections.Generic;
using k8s;
using k8s.Models;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Turquoise.Models;
using System.Linq;
using Microsoft.Rest;
using Microsoft.Extensions.Logging;
using Turquoise.K8s.K8sClients;
using System;
using Newtonsoft.Json.Linq;


namespace Turquoise.K8s.Services
{
    public class K8sService
    {

        private Kubernetes client;
        private IMapper mapper;
        private K8sNamespaceClient namespacesClient;
        private K8sDeploymentClient deploymentsClient;
        private K8sNodeClient nodesClient;
        private K8sPodClient podsClient;
        private K8sServiceClient serviceClient;
        private K8sIngressClient ingressClient;
        private ILogger<K8sService> logger;

        public K8sEventClient EventClient { get; set; }

        public K8sService(IKubernetesClient kubernetesClient, IMapper mapper, ILogger<K8sService> logger)
        {
            this.client = kubernetesClient.Client;
            this.mapper = mapper;
            namespacesClient = new K8sNamespaceClient(this.client);
            deploymentsClient = new K8sDeploymentClient(this.client);
            nodesClient = new K8sNodeClient(this.client);
            podsClient = new K8sPodClient(this.client);
            serviceClient = new K8sServiceClient(this.client);
            ingressClient = new K8sIngressClient(this.client);
            EventClient = new K8sEventClient(this.client, logger);
            this.logger = logger;
        }

        public async Task<List<V1Namespace>> GetNamespaces()
        {

            var items = await namespacesClient.GetAsync();
            return items.ToList();//items.Select(ns => ns.Name()).ToList();
        }

        public async Task<IList<V1Deployment>> GetDeploymentsAsync(string namespaceParam)
        {
            var items = await deploymentsClient.GetAsync(namespaceParam);
            // var dtoitems = mapper.Map<IList<Deployment>>(items);
            return items;//items;
        }


        public async Task<IList<Turquoise.Models.Mongo.DeploymentV1>> GetAllMongoDeploymentsAsync()
        {
            var items = await deploymentsClient.GetAllAsync();
            var dtoitems = mapper.Map<IList<Turquoise.Models.Mongo.DeploymentV1>>(items);
            return dtoitems;
        }


        public async Task<IList<V1Deployment>> GetAllDeploymentsAsync()
        {
            var items = await deploymentsClient.GetAllAsync();
            // var dtoitems = mapper.Map<IList<Deployment>>(items);
            return items;//items;
        }

        public async Task<IList<V1Service>> GetServicesforCron()
        {
            var services = await this.serviceClient.GetAllAsync();
            logger.LogWarning(services.Count + "service count");
            var filtered = services.Where(p => p.Metadata != null && p.Metadata.Annotations != null && p.Metadata.Annotations.Keys.Contains("healthcheck/isalive"));
            return filtered.ToList();
        }

        public async Task<List<Turquoise.Models.Mongo.PodV1>> GetPodsMapped(string namespaceParam)
        {
            var podsv1 = await this.podsClient.GetAsync(namespaceParam);
            var dtoitems = mapper.Map<List<Turquoise.Models.Mongo.PodV1>>(podsv1);
            return dtoitems;
        }

        public async Task<IList<V1Service>> GetServices(string namespaceParam)
        {
            var services = await this.serviceClient.GetAsync(namespaceParam);
            logger.LogWarning(services.Count + "service count");
            // var filtered = services.Where(p => p.Metadata != null && p.Metadata.Annotations != null && p.Metadata.Annotations.Keys.Contains("healthcheck/isalive"));
            return services.ToList();
        }


        public async Task<IList<V1Service>> GetAllServicesAsync()
        {
            var services = await this.serviceClient.GetAllAsync();
            logger.LogWarning(services.Count + "GetAllServicesAsync count");
            return services.ToList();
        }

        public Task<List<Turquoise.Models.Mongo.ServiceV1>> GetAllServicesWithIngressAsync()
        {
            var returnservices = new List<Turquoise.Models.Mongo.ServiceV1>();
            var ingressTask = this.ingressClient.GetAllAsync();
            var serviceTask = this.serviceClient.GetAllAsync();
            var vstask = GetAllVirtualServicesAsync();
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
                //            var ings = ingresses.Where(p => p.Metadata.Namespace() == serviceNamespace && p.Spec.Rules.FirstOrDefault()?.Http.Paths.FirstOrDefault()?.Backend.ServiceName == serviceName);
                //  var ings =    ingresses.Where(p => p.Metadata.Namespace() == serviceNamespace && p.Spec.Rules.All(pp => pp.Http.Paths.All( ppp => ppp.Backend.ServiceName == serviceName)));

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

        public async Task<IList<Extensionsv1beta1Ingress>> GetAllIngressAsync()
        {
            var ingresses = await this.ingressClient.GetAllAsync();
            logger.LogWarning(ingresses.Count + "GetAllServicesAsync count");
            return ingresses.ToList();
        }

        public List<MapServiceIngressPod> MapServiceIngressAndPodsAsync()
        {
            var ingressTask = this.ingressClient.GetAllAsync();
            var serviceTask = this.serviceClient.GetAllAsync();
            // var podTask = this.podsClient.GetAllAsync();
            Task.WaitAll(ingressTask, serviceTask);

            var ingresses = ingressTask.Result;
            var services = serviceTask.Result;
            // var pods = podTask.Result;

            List<MapServiceIngressPod> maps = new List<MapServiceIngressPod>();

            foreach (var service in services)
            {

                var serviceName = service.Name();
                var serviceNamespace = service.Namespace();

                var map = new MapServiceIngressPod();
                map.Service = service;
                maps.Add(map);
                logger.LogInformation("Pods for service: " + service.Metadata.Name);
                logger.LogInformation("=-=-=-=-=-=-=-=-=-=-=");
                if (service.Spec == null || service.Spec.Selector == null)
                {
                    continue;
                }

                var labels = new List<string>();
                foreach (var key in service.Spec.Selector.OrderBy(p => p.Key))
                {
                    labels.Add(key.Key + "=" + key.Value);
                }

                var labelStr = string.Join(",", labels.ToArray());
                logger.LogInformation(labelStr);
                var podList = client.ListNamespacedPod(serviceNamespace, labelSelector: labelStr);

                foreach (var pod in podList.Items)
                {
                    map.Pods.Add(pod);
                }


                var ings = ingresses.Where(p => p.Metadata.Namespace() == serviceNamespace && p.Spec.Rules.FirstOrDefault()?.Http.Paths.FirstOrDefault()?.Backend.ServiceName == serviceName);
                map.ingress = ings.FirstOrDefault();

            }
            return maps;

        }

        public void GetDeploymentDescribe()
        {

        }

        public async Task<List<VirtualServiceV1>> GetVirtualServicesAsync(string namespaceParam)
        {
            List<VirtualServiceV1> items = new List<VirtualServiceV1>();
            var result = await client.ListNamespacedCustomObjectAsync("networking.istio.io", "v1alpha3", namespaceParam, "virtualservices") as JObject;
            var jtokens = result.GetValue("items").AsJEnumerable();
            foreach (JObject jitem in jtokens)
            {
                var host = jitem.SelectToken("spec.hosts[0]").ToString();
                var service = jitem.SelectToken("spec.http[0].route[0].destination.host").ToString();
                var port = jitem.SelectToken("spec.http[0].route[0].destination.port.number").ToString();

                var name = jitem.SelectToken("metadata.name").ToString();
                var namespaceparam = jitem.SelectToken("metadata.namespace").ToString();

                logger.LogCritical(host + " > " + service + ":" + port);
                var item = new VirtualServiceV1 { Host = host, Service = service, Port = port, Name = name, Namespace = namespaceParam };
                items.Add(item);
            }
            return items;
        }

        public async Task<List<VirtualServiceV1>> GetAllVirtualServicesAsync()
        {
            List<VirtualServiceV1> Items = new List<VirtualServiceV1>();
            List<Task<List<VirtualServiceV1>>> tasks = new List<Task<List<VirtualServiceV1>>>();
            var namespaces = await GetNamespaces();
            foreach (var ns in namespaces)
            {
                var task = GetVirtualServicesAsync(ns.Name());
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());
            foreach (var item in tasks)
            {
                Items.AddRange(item.Result);
            }
            return Items;
        }
        public async Task<IList<V1Event>> GetEventsAsync(string namespaceParam)
        {
            var res = await client.ListNamespacedEventAsync(namespaceParam, fieldSelector: "type!=Normal");
            logger.LogCritical("Event founds, Total: " + res.Items.Count);
            return res.Items;
        }

    }
    public class MapServiceIngressPod
    {
        public MapServiceIngressPod()
        {
            Pods = new List<V1Pod>();
        }
        public Extensionsv1beta1Ingress ingress { get; set; }
        public V1Service Service { get; set; }

        public List<V1Pod> Pods { get; set; }
    }


    public class VirtualServiceV1
    {
        public string Host { get; set; }
        public string Service { get; set; }
        public string Port { get; set; }

        public string Name { get; set; }
        public string Namespace { get; set; }
    }

}