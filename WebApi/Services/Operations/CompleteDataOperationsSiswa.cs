using Entites.Data;
using Services.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using Entites.Master;
using InterfaceApi.DataAccess;
using Entites.Request;
using System.Net.Http;
using System.Web.Http;

namespace Services.Operations
{
    internal class CompleteDataOperationsSiswa
    {

        internal static void Initialize(ref CompleteDataSiswa container)
        {
            var data = new siswa()
            {
                id_siswa = container.siswa_data.id_siswa,
                nama_siswa = container.siswa_data.nama_siswa,
                alamat = container.siswa_data.alamat
            };

            Save(container.repository, data);
        }

        //internal static void Initialize(SiswaEntity se, IUnitOfWork _unitOfWork, GenericRequest request)
        //{

        //    var data = new siswa()
        //    {
        //        id_siswa = se.id_siswa,
        //        nama_siswa = se.nama_siswa,
        //        alamat = se.alamat
        //    };

        //    Save(_unitOfWork, data);
        //}

        //internal static void Remove(SiswaEntity se, IUnitOfWork _unitOfWork, GenericRequest request)
        //{
        //    using(var scope = new TransactionScope())
        //    {
        //        string id_siswa = se.id_siswa;

        //        _unitOfWork.SiswaRepository.Delete(x => x.id_siswa == id_siswa);
        //        _unitOfWork.Save();

        //        scope.Complete();
        //    }
        //}

        internal static void Remove(ref CompleteDataSiswa container)
        {
            var id_siswa = container.siswa.id_siswa;
            var siswa = container.repository.SqlQuery<siswa>("select * from siswa where id_siswa='" + id_siswa + "' LIMIT 1;").SingleOrDefault();

            if (siswa != null)
            {
                using (var scope = new TransactionScope())
                {
                    container.repository.SiswaRepository.Delete(x => x.id_siswa == id_siswa);
                    container.repository.Save();

                    scope.Complete();
                }
            }else
            {
                var resp = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadRequest, Content = new StringContent("{\"error\":\"Data Siswa ini tidak ditemukan di database!\"}") };
                resp.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                throw new HttpResponseException(resp);
            }

            
        }

        //internal static void Initialize2(SiswaEntity se, IUnitOfWork _unitOfWork, GenericRequest request)
        //{
        //    var id_siswa = se.id_siswa;
        //    var siswa = _unitOfWork.SqlQuery<siswa>("select * from siswa where id_siswa='" + id_siswa + "' LIMIT 1;").SingleOrDefault();
            
        //    siswa.nama_siswa = !string.IsNullOrEmpty(se.nama_siswa) ? se.nama_siswa : siswa.nama_siswa;
        //    siswa.alamat = !string.IsNullOrEmpty(se.alamat) ? se.alamat : siswa.alamat;

            
        //    _unitOfWork.SiswaRepository.Update(siswa);
        //    _unitOfWork.Save();
        //}

        private static void Save(IUnitOfWork _unitOfWork, siswa siswa)
        {
            var cek_data_sama = _unitOfWork.SiswaRepository.Get(x => x.nama_siswa == siswa.nama_siswa);

            if (cek_data_sama == null)
            {
                using (var scope = new TransactionScope())
                {
                    _unitOfWork.SiswaRepository.Insert(siswa);
                    _unitOfWork.Save();

                    scope.Complete();
                }
            }
            else
            {
                var resp = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadRequest, Content = new StringContent("{\"error\":\"Baka Nama siswa sudah ada!\"}") };
                resp.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                throw new HttpResponseException(resp);
            }

            
        }


        internal static void InitializeUpdate(ref CompleteDataSiswa container)
        {
            var id_siswa = container.siswa.id_siswa;

            var siswa = container.repository.SqlQuery<siswa>("select * from siswa where id_siswa='" + id_siswa + "' LIMIT 1;").SingleOrDefault();

            //Mapper.Map(container.siswa, siswa);

            if(siswa != null)
            {
                siswa.nama_siswa = !string.IsNullOrEmpty(container.siswa.nama_siswa) ? container.siswa.nama_siswa : siswa.nama_siswa;
                siswa.alamat = !string.IsNullOrEmpty(container.siswa.alamat) ? container.siswa.alamat : siswa.alamat;

                container.repository.SiswaRepository.Update(siswa);
                container.repository.Save();
            }
            else
            {
                var resp = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadRequest, Content = new StringContent("{\"error\":\"Data Siswa ini tidak ditemukan di database!\"}") };
                resp.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                throw new HttpResponseException(resp);
            }
           
        }
    }
}
