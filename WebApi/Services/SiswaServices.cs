using InterfaceApi.DataAccess;
using InterfaceApi.Siswa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entites.Request;
using Services.Container;
using Services.Operations;
using Newtonsoft.Json;
using Entites.Master;
using DataAccess;

namespace Services
{
    public class SiswaServices : ISiswaServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public SiswaServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public GenericResponse ConfirmDataSiswa(GenericRequest @params)
        {
            var output = "";

            var se = new SiswaEntity();
            se = JsonConvert.DeserializeObject<SiswaEntity>(@params.json_data);

            CompleteDataOperationsSiswa.Initialize2(se, _unitOfWork, @params);

            output = JsonConvert.SerializeObject(se);
            return new GenericResponse() { data = output };
        }

        public List<SiswaEntity> GetAllSiswa()
        {
            using (var context = new OctopusDBEntities())
            {
                var query = $"select * from siswa";
                var data = context.Database.SqlQuery<SiswaEntity>(query).ToList();
                return data;
            }

        }

        public GenericResponse SaveDataSiswa(GenericRequest @params)
        {
            var output = "";

            var se = new SiswaEntity();
            se = JsonConvert.DeserializeObject<SiswaEntity>(@params.json_data);

            CompleteDataOperationsSiswa.Initialize(se, _unitOfWork, @params);

            output = JsonConvert.SerializeObject(se);

            return new GenericResponse() { data = output };
        }
    }
}
