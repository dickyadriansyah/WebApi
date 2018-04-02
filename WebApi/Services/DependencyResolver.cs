using InterfaceApi.Services;
using InterfaceApi.Siswa;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Resolver;

namespace Services
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IRouteServices, RouteServices>();
            registerComponent.RegisterType<ISiswaServices, SiswaServices>();
        }
    }
}
