using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Turquoise.K8s.Services;

namespace Turquoise.Api.HealthMonitoring.Hubs
{
    [Authorize]
    public class K8sHub : Hub
    {
        private readonly ILogger<K8sHub> logger;
        private readonly K8sService k8sService;

        public K8sHub(ILogger<K8sHub> logger, K8sService k8sService)
        {
            this.logger = logger;
            this.k8sService = k8sService;

        }

        public override async Task OnConnectedAsync()
        {
            // await Clients.Client(Context.ConnectionId).InvokeAsync("SetUsersOnline", await GetUsersOnline());
            // var iden = this.Context.User.Identity;
            await base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public void ConnectGetLogs(string podName, string namespaceParam)
        {
            var mes = new LogMessage { Message = "Connecting to Logs" };
            Clients.Caller.SendAsync("ConnectGetLogs", mes);

        }


        public void DisconnectGetLogs(string podName, string namespaceParam)
        {
            var mes = new LogMessage { Message = "Disconnecting to Logs" };
            Clients.Caller.SendAsync("ConnectGetLogs", mes);
        }



    }

    public class LogCollector
    {
        public IClientProxy Caller { get; set; }
        public string PodName { get; set; }
        public string Namespace { get; set; }

    }

    public class LogMessage
    {
        public string Message { get; set; }
    }
}