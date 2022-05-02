using System;

using AgentOrders.Logic;

namespace AgentOrders.WebService
{
    public static class AppConfig
    {
        public static void Initialize() 
        {
            var scriptFileName = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/InitDatabase.sql");
            new LogicInitializer().Initialize(scriptFileName);
        }
    }
}