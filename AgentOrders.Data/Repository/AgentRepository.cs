using System;
using System.Data.SqlClient;

using AgentOrders.Data.Utils;

namespace AgentOrders.Data.Repository
{
    public class AgentRepository
    {
        private readonly string connectionString;
        private SqlHelper sqlHelper => new SqlHelper(connectionString);

        public AgentRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public string GetHighestAdvanceAgentCode(int year)
        {
            return sqlHelper.GetEntity("GetHighestAdvanceAgent", new SqlParameter("@year", year), reader => reader.GetString(0).Trim());
        }
    }
}
