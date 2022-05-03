using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using AgentOrders.Domain;

namespace AgentOrders.Data
{
    public class OrderRepository
    {
        private readonly string connectionString;
        private SqlHelper sqlHelper => new SqlHelper(connectionString);

        public OrderRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<AgentOrderModel> GetOrdersByIndex(IEnumerable<string> agentCodes, int orderIndex)
        {
            var parameters = new[] 
            {
                SqlParameterUtils.CreateStrListParameter("@agentCodes", agentCodes),
                new SqlParameter("@orderIndex", orderIndex)
            };
            return sqlHelper.GetEntityList("GetOrdersByIndex", parameters, GetAgentOrderModelByReader);
        }

        private AgentOrderModel GetAgentOrderModelByReader(IDataReader reader) 
        {
            return new AgentOrderModel 
            {
                OrderNum = (int)reader["ORD_NUM"],
                OrderAmount = (decimal)(double)reader["ORD_AMOUNT"],
                AdvanceAmount = (decimal)(double)reader["ADVANCE_AMOUNT"],
                OrderDate = (DateTime)reader["ORD_DATE"],
                CustomerCode = (string)reader["CUST_CODE"],
                AgentCode = (string)reader["AGENT_CODE"],
                OrderDescription = (string)reader["ORD_DESCRIPTION"],
            };
        }
    }
}
