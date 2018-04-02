using InterfaceApi.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Resolver;

namespace DataAccess
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IUnitOfWork, Helper.UnitOfWork>();
        }
    }
}
