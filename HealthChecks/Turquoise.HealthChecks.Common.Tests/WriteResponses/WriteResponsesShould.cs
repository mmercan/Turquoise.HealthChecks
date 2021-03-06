using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Turquoise.HealthChecks.Common;
using Moq;

namespace Turquoise.HealthChecks.Common.Tests.WriteResponse
{
    public class WriteResponsesShould
    {
        public void CreateAWriteListResponse()
        {

            var moqContext = new Mock<HttpContext>();
            var httpContext = moqContext.Object;
            HealthReport result = new HealthReport(null, TimeSpan.FromMinutes(5));

            WriteResponses.WriteListResponse(httpContext, result);
        }
    }
}