using System;

namespace AgentOrders.Domain
{
    public class AgentOrderModel
    {
        public int OrderNum { get; set; }
        public decimal OrderAmount { get; set; }
        public decimal AdvanceAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerCode { get; set; }
        public string AgentCode { get; set; }
        public string OrderDescription { get; set; }
    }
}
