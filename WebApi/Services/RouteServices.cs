using InterfaceApi.DataAccess;
using InterfaceApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entites.Request;
using InterfaceApi.Siswa;
using InterfaceApi.Order;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace Services
{
    internal class RouteServices :IRouteServices
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ISiswaServices _siswaSerivices;
        private readonly IOrderBukuService _orderBukuService;

        public RouteServices(IUnitOfWork unitOfWork, ISiswaServices siswaServices, IOrderBukuService orderBukuService)
        {
            _unitOfWork = unitOfWork;
            _siswaSerivices = siswaServices;
            _orderBukuService = orderBukuService;
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
            else if (action.Trim().Equals("SaveDataSiswa", StringComparison.InvariantCultureIgnoreCase))
            {
                var result = _siswaSerivices.SaveDataSiswa(request);
                response.data = result.data;
            }
            else if(action.Trim().Equals("DeleteDataSiswa", StringComparison.InvariantCultureIgnoreCase))
            {
                var result = _siswaSerivices.DeleteSiswa(request);
                response.data = result.data;
            }
            else if (action.Trim().Equals("InsertOrder", StringComparison.InvariantCultureIgnoreCase))
            {
                var result = _orderBukuService.OrderBukuProses(request);
                response.data = result.data;
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
