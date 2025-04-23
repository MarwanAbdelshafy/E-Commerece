using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraction;
using AutoMapper;
using Domain.Contracts;

namespace Services
{
    public class ServicesManager(IUnitOfWorK unitOfWorK,IMapper mapper) : IServicesManager
    {
        private readonly Lazy<IProductServices> _lazyProductServices = new Lazy<IProductServices>(() => new ProductServices(unitOfWorK,mapper));  
        public IProductServices ProductServices => _lazyProductServices.Value;
    }
}
