using Entites.Data;
using Entites.Master;
using Entites.Request;
using InterfaceApi.DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Services.Container
{
    internal class CompleteDataSiswa
    {
        internal CompleteDataSiswa()
        {
            siswa = new siswa();
        }

        internal CompleteDataSiswa(GenericRequest request)
        {
            this.request = request;
            siswa_data = JsonConvert.DeserializeObject<SiswaEntity>(request.json_data);
            siswa_data.EmptyStringtoNull();
            siswa = new siswa
            {
                id_siswa = siswa_data.id_siswa,
                nama_siswa = siswa_data.nama_siswa,
                alamat = siswa_data.alamat
            };
        }

        internal SiswaEntity siswa_data { get; set; }

        //internal DataSiswa data_siswa { get; set; }
        
        internal IUnitOfWork repository { get; set; }

        internal GenericRequest request { get; set; }

        internal siswa siswa { get; set; }
    }
}
