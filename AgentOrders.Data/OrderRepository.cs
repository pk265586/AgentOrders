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

        public List<AgentModel> GetAgentsByMinOrders(int minCount, int year)
        {
            var parameters = new[]
            {
                new SqlParameter("@minCount", minCount),
                new SqlParameter("@year", year)
            };
            return sqlHelper.GetEntityList("GetAgentsByMinOrders", parameters, GetAgentModelByReader);
        }

        private AgentOrderModel GetAgentOrderModelByReader(IDataReader reader) 
        {
            return new AgentOrderModel 
            {
                OrderNum = (int)reader["ORD_NUM"],
                OrderAmount = (decimal)(double)reader["ORD_AMOUNT"],
                AdvanceAmount = (decimal)(double)reader["ADVANCE_AMOUNT"],
                OrderDate = (DateTime)reader["ORD_DATE"],
                CustomerCode = reader["CUST_CODE"].ToString().Trim(),
                AgentCode = reader["AGENT_CODE"].ToString().Trim(),
                OrderDescription = reader["ORD_DESCRIPTION"].ToString().Trim(),
            };
        }

        private AgentModel GetAgentModelByReader(IDataReader reader)
        {
            return new AgentModel
            {
                Code = reader["AGENT_CODE"].ToString().Trim(),
                Name = reader["AGENT_NAME"].ToString().Trim(),
                WorkingArea = reader["WORKING_AREA"].ToString().Trim(),
                Commission = (int)reader["COMMISSION"],
                PhoneNo = reader["PHONE_NO"].ToString().Trim(),
                Country = reader["COUNTRY"].ToString().Trim(),
            };
        }
    }
}
