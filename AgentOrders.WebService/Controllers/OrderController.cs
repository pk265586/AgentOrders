using System;
using System.Linq;
using System.Web.Http;

using AgentOrders.Logic.Abstract;
using AgentOrders.WebService.Models;

namespace AgentOrders.WebService.Controllers
{
    public class OrderController : ApiController
    {
        IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        /// <summary>
        /// Returns list of orders, one per agent, where each item is N-th oldest order of one agent. If an agent has less than N orders, that agent is not listed.
        /// <para> Sample url for testing: https://localhost:44358/api/Order/OrdersByIndex?agentCodes=A004,A009&orderIndex=2 </para>
        /// </summary>
        /// <param name="agentCodes"> Agent codes filter </param>
        /// <param name="orderIndex"> Order index to return for each agent </param>
        /// <returns> The list of the agents with orders, no more than 1 order per agent </returns>
        [HttpGet]
        public OrdersByIndexResponseModel[] OrdersByIndex(string agentCodes, int orderIndex)
        {
            var codesArray = agentCodes.Split(',');
            return orderService.GetOrdersByIndex(codesArray, orderIndex)
                .Select(m => ModelConvertor.AgentOrderToResponseModel(m))
                .ToArray();
        }

        /// <summary>
        /// Returns list of agents who have orders count in the given year more or equal to the given number.
        /// <para> Sample url for testing: https://localhost:44358/api/Order/AgentsByMinOrders?minCount=3&year=2021 </para>
        /// </summary>
        /// <param name="minCount"> Minimum number of orders of an agent </param>
        /// <param name="year"> A year to check </param>
        /// <returns> The list of the agents in question </returns>
        [HttpGet]
        public ListByMinOrdersResponseModel[] AgentsByMinOrders(int minCount, int year)
        {
            return orderService.GetAgentsByMinOrders(minCount, year)
                .Select(m => ModelConvertor.AgentToResponseModel(m))
                .ToArray();
        }
    }
}
