using System;

namespace AgentOrders.Data
{
    public class DataInitializer
    {
        public void Initialize(string connectionString, string scriptFileName)
        {
            var helper = new SqlHelper(connectionString);
            if (!helper.IsTableExist("AGENTS"))
            {
                helper.RunScript(scriptFileName);
            }
        }
    }
}
