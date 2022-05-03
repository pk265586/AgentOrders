using System;

namespace AgentOrders.Logic.Abstract
{
    public interface IAgentService
    {
        string GetHighestAdvanceAgentCode(int year);
    }
}
