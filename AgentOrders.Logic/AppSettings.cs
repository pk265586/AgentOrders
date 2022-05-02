using System;
using System.Configuration;

namespace AgentOrders.Logic
{
    public static class AppSettings
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["Main"].ConnectionString;
    }
}
