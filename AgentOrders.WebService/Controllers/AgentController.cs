using System;
using System.Web.Http;

using AgentOrders.Logic.Abstract;

namespace AgentOrders.WebService.Controllers
{
    public class AgentController : ApiController
    {
        IAgentService agentService;

        public AgentController(IAgentService agentService) 
        {
            this.agentService = agentService;
        }

        /// <summary>
        /// Returns the code of an agent who has the highest sum of ADVANCE_AMOUNT in the first quarter of the specific year sent in the parameter.
        /// <para> Sample url for testing: https://localhost:44358/api/Agent/HighestAdvance?year=2021 </para>
        /// </summary>
        /// <param name="year"> A year to check. </param>
        /// <returns> The AGENT_CODE value of the agent in question </returns>
        [HttpGet]
        public object HighestAdvance(int year)
        {
            var agentCode = agentService.GetHighestAdvanceAgentCode(year);
            return new { AgentCode = agentCode };
        }

    }
}
