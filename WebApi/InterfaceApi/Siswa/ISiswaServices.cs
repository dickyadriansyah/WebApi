using Entites.Master;
using Entites.Request;
using System.Collections.Generic;

namespace InterfaceApi.Siswa
{
    public interface ISiswaServices
    {
        GenericResponse ConfirmDataSiswa(GenericRequest @params);

        List<SiswaEntity> GetAllSiswa();
    }
}
