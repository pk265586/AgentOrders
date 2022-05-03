using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AgentOrders.Data;

namespace AgentOrders.Logic
{
    public class AgentService
    {
        public string GetHighestAdvanceAgentCode(int year)
        {
            return new AgentRepository(AppSettings.ConnectionString).GetHighestAdvanceAgentCode(year);
        }
    }
}
