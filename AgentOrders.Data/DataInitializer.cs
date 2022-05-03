using System;

namespace AgentOrders.Data
{
    public class DataInitializer
    {
        public void Initialize(string connectionString, string scriptFileName)
        {
            if (!SqlUtils.IsTableExist(connectionString, "AGENTS"))
            {
                SqlUtils.RunScript(connectionString, scriptFileName);
            }
        }
    }
}
