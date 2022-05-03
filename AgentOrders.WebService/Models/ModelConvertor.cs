using System;

using AgentOrders.Domain;

namespace AgentOrders.WebService.Models
{
    public static class ModelConvertor
    {
        public static OrdersByIndexResponseModel AgentOrderToResponseModel(AgentOrderModel source) 
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

        public static ListByMinOrdersResponseModel AgentToResponseModel(AgentModel source)
        {
            return new ListByMinOrdersResponseModel
            {
                AgentCode = source.Code,
                AgentName = source.Name,
                PhoneNo = source.PhoneNo
            };
        }
    }
}