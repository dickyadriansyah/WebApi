using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Request
{
    public class BasicResponse
    {
        public String rc { get; set; }

        public String message { get; set; }

        public String stacktrace { get; set; }
    }
}
