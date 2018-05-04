using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceApi.Utilities
{
    public interface IJaroWinklerDistance
    {
        double proximity(string aString1, string aString2);
    }
}
