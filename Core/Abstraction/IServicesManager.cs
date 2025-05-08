using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction
{
    public interface IServicesManager
    {
        public IProductServices ProductServices { get; }

        public IBsaketServices BasketServices { get; }

        public IAuthenticationServices AuthenticationServices { get; }


    }
}
