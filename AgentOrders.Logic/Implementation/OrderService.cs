using System;
using System.Collections.Generic;

using AgentOrders.Domain;
using AgentOrders.Data.Repository;
using AgentOrders.Logic.Abstract;

namespace AgentOrders.Logic.Implementation
{
    public class OrderService : IOrderService
    {
        public List<AgentOrderModel> GetOrdersByIndex(string[] agentCodes, int orderIndex)
        {
            return new OrderRepository(AppSettings.ConnectionString).GetOrdersByIndex(agentCodes, orderIndex);
        }

        public List<AgentModel> GetAgentsByMinOrders(int minCount, int year) 
        {
            return new OrderRepository(AppSettings.ConnectionString).GetAgentsByMinOrders(minCount, year);
        }
    }
}
