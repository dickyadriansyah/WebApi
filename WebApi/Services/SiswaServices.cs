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
using InterfaceApi.Utilities;

namespace Services
{
    public class SiswaServices : ISiswaServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private IJaroWinklerDistance _similarity;

        public SiswaServices(IUnitOfWork unitOfWork, IJaroWinklerDistance similarity)
        {
            _unitOfWork = unitOfWork;
            _similarity = similarity;
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

            var similarity = _similarity.proximity(container.siswa.nama_siswa.Trim(), container.siswa_data.nama_siswa.Trim()) * 100;
            
            try
            {
                if (container != null)
                {
                    if(similarity > 0)
                    {
                        response = true;
                    }
                    
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