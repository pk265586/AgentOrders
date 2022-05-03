using System;
using System.Web.Http;

using Unity;

using AgentOrders.Logic.Abstract;
using AgentOrders.Logic.Implementation;

namespace AgentOrders.WebService.Infrastructure
{
    public static class CompositionRoot
    {
        public static void Compose(HttpConfiguration config) 
        {
            var container = new UnityContainer();

            container.RegisterType<IAgentService, AgentService>()
                .RegisterType<IOrderService, OrderService>();

            config.DependencyResolver = new UnityResolver(container);
        }
    }
}