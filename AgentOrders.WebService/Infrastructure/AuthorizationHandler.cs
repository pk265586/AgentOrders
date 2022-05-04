using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using AgentOrders.Logic;

namespace AgentOrders.WebService.Infrastructure
{
    public class AuthorizationHandler : DelegatingHandler
    {
        const string ApiKey = "XApiKey";

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var mainKey = AppSettings.ApiKey;
            if (!string.IsNullOrEmpty(mainKey))
            {
                if (!request.Headers.TryGetValues(ApiKey, out var values))
                    return request.CreateResponse(HttpStatusCode.Unauthorized, "Missing API key header!");

                var requestKey = values.FirstOrDefault();

                if (!mainKey.Equals(requestKey))
                    return request.CreateResponse(HttpStatusCode.Forbidden, "Invalid API key!");
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}