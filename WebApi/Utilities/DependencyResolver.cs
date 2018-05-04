using InterfaceApi.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Common;
using Utilities.Resolver;

namespace Utilities
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IJaroWinklerDistance, JaroWinklerDistance>();
        }
    }
}
