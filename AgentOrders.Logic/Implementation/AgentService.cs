using System;
using System.Collections.Generic;

using AgentOrders.Domain;
using AgentOrders.Data;
using AgentOrders.Logic.Abstract;

namespace AgentOrders.Logic.Implementation
{
    public class AgentService : IAgentService
    {
        public string GetHighestAdvanceAgentCode(int year)
        {
            return new AgentRepository(AppSettings.ConnectionString).GetHighestAdvanceAgentCode(year);
        }


    }
}
