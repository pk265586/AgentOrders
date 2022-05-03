using System;
using System.Collections.Generic;

using AgentOrders.Domain;

namespace AgentOrders.Logic.Abstract
{
    public interface IOrderService
    {
        List<AgentOrderModel> GetOrdersByIndex(string[] agentCodes, int orderIndex);
    }
}
