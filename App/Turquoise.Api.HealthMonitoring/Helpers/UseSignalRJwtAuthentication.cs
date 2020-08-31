using System.Linq;
using Microsoft.AspNetCore.Builder;

namespace Turquoise.Api.HealthMonitoring.Helpers
{
    public static class extesions
    {
        public static void UseSignalRJwtAuthentication(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                if (string.IsNullOrWhiteSpace(context.Request.Headers["Authorization"]))
                {
                    if (context.Request.QueryString.HasValue)
                    {
                        var token = context.Request.QueryString.Value.Split('&').SingleOrDefault(x => x.Contains("authorization"))?.Split('=')[1];
                        if (!string.IsNullOrWhiteSpace(token))
                        {
                            context.Request.Headers.Add("Authorization", new[] { $"Bearer {token}" });
                        }
                    }
                }
                await next.Invoke();
            });
        }
    }
}