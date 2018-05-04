using Entites.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceApi.Order
{
    public interface IOrderBukuService
    {
        GenericResponse OrderBukuProses(GenericRequest @params);
    }
}
