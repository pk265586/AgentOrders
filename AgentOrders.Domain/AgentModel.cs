using System;

namespace AgentOrders.Domain
{
    public class AgentModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string WorkingArea { get; set; }
        public int Commission { get; set; }
        public string PhoneNo { get; set; }
        public string Country { get; set; }
    }
}
