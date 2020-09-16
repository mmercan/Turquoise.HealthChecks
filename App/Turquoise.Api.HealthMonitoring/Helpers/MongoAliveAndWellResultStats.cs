using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using Turquoise.Common.Mongo;
using Turquoise.Models.Mongo;

namespace Turquoise.Api.HealthMonitoring.Helpers
{
    public class MongoAliveAndWellResultStats
    {
        private MangoBaseRepo<ServiceV1> serviceMongoRepo;
        private MangoBaseRepo<AliveAndWellResult> aliveAndWellResultRepo;
        private IMemoryCache cache;

        public MongoAliveAndWellResultStats(
            MangoBaseRepo<Turquoise.Models.Mongo.ServiceV1> serviceMongoRepo,
            MangoBaseRepo<Turquoise.Models.Mongo.AliveAndWellResult> aliveAndWellResultRepo,
            IMemoryCache cache
        )
        {
            this.serviceMongoRepo = serviceMongoRepo;
            this.aliveAndWellResultRepo = aliveAndWellResultRepo;
            this.cache = cache;
        }

        public AliveAndWellResultStat GetResultStatWithCache(string namespaceParam)
        {
            AliveAndWellResultStat cacheEntry;
            if (!cache.TryGetValue(namespaceParam + "stats", out cacheEntry))
            {
                cacheEntry = GetResultStat(namespaceParam);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(15));
                cache.Set(namespaceParam + "stats", cacheEntry, cacheEntryOptions);
            }
            return cacheEntry;
        }
        public AliveAndWellResultStat GetResultStat(string namespaceParam)
        {

            var stats = new AliveAndWellResultStat();

            var serviceBuilder = Builders<ServiceV1>.Filter;
            var serviceFilter = serviceBuilder.Eq(x => x.Namespace, namespaceParam) & serviceBuilder.Eq(x => x.Deleted, false);
            var totalservicesTask = serviceMongoRepo.Items.CountDocumentsAsync(serviceFilter);
            // var totalservices = serviceMongoRepo.Find(p => p.Deleted == false && p.Namespace == namespaceParam).Count();


            var unhealthyStatsBuilder = Builders<AliveAndWellResult>.Filter;
            var unhealthyStatsFilter = unhealthyStatsBuilder.Eq(x => x.ServiceNamespace, namespaceParam) & unhealthyStatsBuilder.Ne(x => x.Status, "OK") & unhealthyStatsBuilder.Gt(x => x.CreationTime, DateTime.UtcNow.AddDays(-1));
            var unhealthystatsTask = aliveAndWellResultRepo.Items.CountDocumentsAsync(unhealthyStatsFilter);
            //var unhealthystats = aliveAndWellResultRepo.Find(p => p.ServiceNamespace == namespaceParam && p.Status != "OK" && p.CreationTime > DateTime.UtcNow.AddDays(-1)).Count();



            var healthyStatsBuilder = Builders<AliveAndWellResult>.Filter;
            var healthyStatsFilter = unhealthyStatsBuilder.Eq(x => x.ServiceNamespace, namespaceParam) & healthyStatsBuilder.Eq(x => x.Status, "OK") & healthyStatsBuilder.Gt(x => x.CreationTime, DateTime.UtcNow.AddDays(-1));
            var healthystatsTask = aliveAndWellResultRepo.Items.CountDocumentsAsync(healthyStatsFilter);
            //   var healthystats = aliveAndWellResultRepo.Find(p => p.ServiceNamespace == namespaceParam && p.Status == "OK" && p.CreationTime > DateTime.UtcNow.AddDays(-1)).Count();

            var unhealthyservTask = aliveAndWellResultRepo.FindAsync(unhealthyStatsFilter);

            //.Select(p => p.ServiceName).Distinct().ToList();
            // var unhealthyserv = aliveAndWellResultRepo.Find(p => p.ServiceNamespace == namespaceParam && p.Status != "OK" && p.CreationTime > DateTime.UtcNow.AddDays(-2)).Select(p => p.ServiceName).Distinct().ToList();


            Task.WaitAll(totalservicesTask, unhealthystatsTask, healthystatsTask, unhealthyservTask);

            stats.syncDate = DateTime.UtcNow;

            stats.AllServices = totalservicesTask.Result;

            stats.HealthyRunsOnToday = healthystatsTask.Result;
            stats.UnhealthyRunsOnToday = unhealthystatsTask.Result;

            stats.AllRunsOnToday = stats.UnhealthyRunsOnToday + stats.HealthyRunsOnToday;
            stats.UnhealthyServicesToday = unhealthyservTask.Result.Select(p => p.ServiceName).Distinct().ToList(); ;

            //var stats2 = aliveAndWellResultRepo.Find(p => p.ServiceNamespace == namespaceParam && p.CreationTime > DateTime.UtcNow.AddDays(-1)).Distinct();



            return stats;


        }
    }
}