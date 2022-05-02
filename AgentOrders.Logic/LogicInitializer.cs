using System;

using AgentOrders.Data;

namespace AgentOrders.Logic
{
    public class LogicInitializer
    {
        public void Initialize(string scriptFileName) 
        {
            new DataInitializer().Initialize(AppSettings.ConnectionString, scriptFileName);
        }
    }
}
