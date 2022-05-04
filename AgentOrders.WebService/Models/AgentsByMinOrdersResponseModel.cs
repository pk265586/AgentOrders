using System;

namespace AgentOrders.WebService.Models
{
    public class AgentsByMinOrdersResponseModel
    {
        public string AgentCode { get; set; }
        public string AgentName { get; set; }
        public string PhoneNo { get; set; }
    }
}