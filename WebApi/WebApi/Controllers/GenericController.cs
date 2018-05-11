using Entites.Request;
using InterfaceApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    [Authorize]
    public class GenericController : ApiController
    {

        private readonly IRouteServices _routeServices;

        public GenericController(IRouteServices routeServices)
        {
            _routeServices = routeServices;
        }

        [Route("v1/Generic")]
        [HttpPost]
        public GenericResponse Generic(GenericRequest request)
        {
            return _routeServices.ServiceDispatcher(request);
        }

    }
}
