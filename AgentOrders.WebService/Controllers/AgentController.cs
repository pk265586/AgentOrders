using System;
using System.Linq;
using System.Web.Http;

using AgentOrders.Logic.Abstract;
using AgentOrders.WebService.Models;

namespace AgentOrders.WebService.Controllers
{
    public class AgentController : ApiController
    {
        IAgentService agentService;
        IOrderService orderService;

        public AgentController(IAgentService agentService, IOrderService orderService) 
        {
            this.agentService = agentService;
            this.orderService = orderService;
        }

        [HttpGet]
        [Route("~/api/Agent/Test")]
        public string Test()
        {
            return "Test is called!";
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

        /// <summary>
        /// Returns list of agents, joined with N-th oldest order for each agent. If an agent has less than N orders, returns empty order values for that agent (left join).
        /// <para> Sample url for testing: https://localhost:44358/api/Agent/OrdersByIndex?agentCodes=A004,A009&orderIndex=2 </para>
        /// </summary>
        /// <param name="agentCodes"> Agent codes filter </param>
        /// <param name="orderIndex"> Order index to return for each agent </param>
        /// <returns> The list of the agents with orders, no more than 1 order per agent </returns>
        [HttpGet]
        public OrdersByIndexResponseModel[] OrdersByIndex(string agentCodes, int orderIndex)
        {
            var codesArray = agentCodes.Split(',');
            return orderService.GetOrdersByIndex(codesArray, orderIndex).
                Select(m => ModelConvertor.ConvertOrderByIndex(m))
                .ToArray();
        }

        /// <summary>
        /// Returns list of agents who have orders count in the given year more or equal to the given number.
        /// <para> Sample url for testing: https://localhost:44358/api/Agent/ListByMinOrders?minCount=5&year=2021 </para>
        /// </summary>
        /// <param name="minCount"> Minimum number of orders of an agent </param>
        /// <param name="year"> A year to check </param>
        /// <returns> The list of the agents in question </returns>
        [HttpGet]
        public ListByMinOrdersResponseModel[] ListByMinOrders(int minCount, int year) 
        {
            return new[] 
            {
                new ListByMinOrdersResponseModel{ AgentCode = "aaa", AgentName = "bbb", PhoneNo = "123" }
            };
        }
    }
}
