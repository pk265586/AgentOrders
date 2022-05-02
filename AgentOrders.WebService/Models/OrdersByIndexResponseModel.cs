using System;

namespace AgentOrders.WebService.Models
{
    public class OrdersByIndexResponseModel
    {
        public int OrderNum { get; set; }
        public decimal Amount { get; set; }
        public decimal AdvanceAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerCode { get; set; }
        public string AgentCode { get; set; }
        public string Description { get; set; }
    }
}