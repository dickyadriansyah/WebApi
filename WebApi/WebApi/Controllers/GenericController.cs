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
            //string key = string.Empty;
            return _routeServices.ServiceDispatcher(request);
        }

    }
}
