using InterfaceApi.DataAccess;
using InterfaceApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entites.Request;
using InterfaceApi.Siswa;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace Services
{
    internal class RouteServices :IRouteServices
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ISiswaServices _siswaSerivices;

        public RouteServices(IUnitOfWork unitOfWork, ISiswaServices siswaServices)
        {
            _unitOfWork = unitOfWork;
            _siswaSerivices = siswaServices;
        }

        public GenericResponse ServiceDispatcher(GenericRequest request)
        {
            var response = new GenericResponse();
            var action = request.action;
            if(action.Trim().Equals("SubmitSiswa", StringComparison.InvariantCultureIgnoreCase))
            {
                var result = _siswaSerivices.ConfirmDataSiswa(request);
                response.data = result.data;
            }
            else if(action.Trim().Equals("GetListDataSiswa", StringComparison.InvariantCultureIgnoreCase))
            {
                var listSiswa = _siswaSerivices.GetAllSiswa();
                response.data = JsonConvert.SerializeObject(listSiswa);
            }
            else
            {
                var resp = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadRequest, Content = new StringContent("{\"error\":\"Action Not Found\"}") };
                resp.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                throw new HttpResponseException(resp);
            }

            response.message = "success";
            response.rc = "00";
            response.stacktrace = string.Empty;
            return response;
        }
    }
}
