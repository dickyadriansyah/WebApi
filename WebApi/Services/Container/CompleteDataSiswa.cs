using Entites.Data;
using Entites.Request;
using InterfaceApi.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Container
{
    internal class CompleteDataSiswa
    {

        internal CompleteDataSiswa(GenericRequest request)
        {
            this.request = request;
            siswa = new siswa();
        }

        internal GenericRequest request;

        internal siswa siswa;

        internal IUnitOfWork repository { get; set; }
    }
}
