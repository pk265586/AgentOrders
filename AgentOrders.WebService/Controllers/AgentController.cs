using System;
using System.Web.Http;

namespace AgentOrders.WebService.Controllers
{
    public class AgentController : ApiController
    {
        [HttpGet]
        [Route("~/api/Agent/Test")]
        public string Test()
        {
            return "Test is called!";
        }

    }
}
