using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BsaketNotFoundException(string key ) : NotFoundException($"Basket with Key {key} Is Not Found")
    {

    }
}
