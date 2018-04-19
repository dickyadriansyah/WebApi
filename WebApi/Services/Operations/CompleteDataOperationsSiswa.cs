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

namespace Services.Operations
{
    internal class CompleteDataOperationsSiswa
    {

        internal static void Initialize(SiswaEntity se, IUnitOfWork _unitOfWork, GenericRequest request)
        {

            var data = new siswa()
            {
                id_siswa = se.id_siswa,
                nama_siswa = se.nama_siswa,
                alamat = se.alamat
            };

            Save(_unitOfWork, data);
        }

        internal static void Initialize2(SiswaEntity se, IUnitOfWork _unitOfWork, GenericRequest request)
        {
            var id_siswa = se.id_siswa;
            var siswa = _unitOfWork.SqlQuery<siswa>("select * from siswa where id_siswa='" + id_siswa + "' LIMIT 1;").SingleOrDefault();
            
            siswa.nama_siswa = !string.IsNullOrEmpty(se.nama_siswa) ? se.nama_siswa : siswa.nama_siswa;
            siswa.alamat = !string.IsNullOrEmpty(se.alamat) ? se.alamat : siswa.alamat;

            
            _unitOfWork.SiswaRepository.Update(siswa);
            _unitOfWork.Save();
        }

        private static void Save(IUnitOfWork _unitOfWork, siswa siswa)
        {
            using(var scope = new TransactionScope())
            {
                _unitOfWork.SiswaRepository.Insert(siswa);
                _unitOfWork.Save();

                scope.Complete();
            }
        }

    }
}
