using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entites.Request;
using InterfaceApi.Order;
using InterfaceApi.DataAccess;
using Entites.Master;
using Newtonsoft.Json;

namespace Services
{
    public class OrderBukuService : IOrderBukuService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderBukuService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public GenericResponse OrderBukuProses(GenericRequest @params)
        {
            var output = false;

            var order_entity = new OrderBukuEntity();
            order_entity = JsonConvert.DeserializeObject<OrderBukuEntity>(@params.json_data);

            output = executeInsertJson(new { order_entity.id_siswa, order_entity.list_nama_buku });

            var response = JsonConvert.SerializeObject(output);
            return new GenericResponse() { data = response };
        }


        public bool executeInsertJson(object data)
        {
            var output = false;
            try
            {
                var doneQuery = "insert into json_test (data) values('"+JsonConvert.SerializeObject(data)+ "'::jsonb);";
                _unitOfWork.ExecuteSqlCommand(doneQuery);
                _unitOfWork.Save();

                output = true;
            }
            catch (Exception ex)
            {
                throw ex;   
            }
            return output;
        }
    }
}
