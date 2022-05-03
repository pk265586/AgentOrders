using System;

using AgentOrders.Domain;

namespace AgentOrders.WebService.Models
{
    public static class ModelConvertor
    {
        public static OrdersByIndexResponseModel ConvertOrderByIndex(AgentOrderModel source) 
        {
            return new OrdersByIndexResponseModel
            {
                OrderNum = source.OrderNum,
                Amount = source.OrderAmount,
                AdvanceAmount = source.AdvanceAmount,
                OrderDate = source.OrderDate,
                CustomerCode = source.CustomerCode,
                AgentCode = source.AgentCode,
                Description = source.OrderDescription,
            };
        }
    }
}