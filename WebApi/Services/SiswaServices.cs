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
            var response = false;
            var container = new CompleteDataSiswa(@params) { repository = _unitOfWork };
           

            CompleteDataOperationsSiswa.InitializeUpdate(ref container);

            try
            {
                if (container != null)
                {
                    response = true;
                }
            }
            catch (Exception)
            {

                throw;
            }

            var output = JsonConvert.SerializeObject(response);
            return new GenericResponse() { data = output };
        }



        //public GenericResponse ConfirmDataSiswa(GenericRequest @params)
        //{
        //    var output = "";

        //    var se = new SiswaEntity();
        //    se = JsonConvert.DeserializeObject<SiswaEntity>(@params.json_data);

        //    CompleteDataOperationsSiswa.Initialize2(se, _unitOfWork, @params);

        //    output = JsonConvert.SerializeObject(se);
        //    return new GenericResponse() { data = output };
        //}

        public GenericResponse DeleteSiswa(GenericRequest @params)
        {
            var response = false;
            var container = new CompleteDataSiswa(@params) { repository = _unitOfWork };

            CompleteDataOperationsSiswa.Remove(ref container);

            try
            {
                if (container != null)
                {
                    response = true;
                }
            }
            catch (Exception)
            {

                throw;
            }

            var output = JsonConvert.SerializeObject(response);
            return new GenericResponse() { data = output };

        }

        //public GenericResponse DeleteSiswa(GenericRequest @params)
        //{
        //    var output = "";

        //    var se = new SiswaEntity();
        //    se = JsonConvert.DeserializeObject<SiswaEntity>(@params.json_data);

        //    CompleteDataOperationsSiswa.Remove(se, _unitOfWork, @params);

        //    output = JsonConvert.SerializeObject(se);

        //    return new GenericResponse() { data = output };

        //}

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
            var response = false;
            var container = new CompleteDataSiswa(@params) { repository = _unitOfWork };



            CompleteDataOperationsSiswa.Initialize(ref container);

            try
            {
                if (container != null)
                {
                    response = true;
                }
            }
            catch (Exception)
            {

                throw;
            }

            var output = JsonConvert.SerializeObject(response);
            return new GenericResponse() { data = output };
        }

        //public GenericResponse SaveDataSiswa(GenericRequest @params)
        //{
        //    var output = "";

        //    var se = new SiswaEntity();
        //    se = JsonConvert.DeserializeObject<SiswaEntity>(@params.json_data);

        //    CompleteDataOperationsSiswa.Initialize(se, _unitOfWork, @params);

        //    output = JsonConvert.SerializeObject(se);

        //    return new GenericResponse() { data = output };
        //}
    }
}