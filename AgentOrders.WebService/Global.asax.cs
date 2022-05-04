using System;
using System.Web.Http;

using AgentOrders.WebService.Infrastructure;

namespace AgentOrders.WebService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.MessageHandlers.Add(new AuthorizationHandler());
            AppConfig.Initialize();
        }
    }
}
