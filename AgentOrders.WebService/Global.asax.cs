using System;
using System.Web.Http;

namespace AgentOrders.WebService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AppConfig.Initialize();
        }
    }
}
