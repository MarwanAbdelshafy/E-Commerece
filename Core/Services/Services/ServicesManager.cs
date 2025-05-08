using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraction;
using AutoMapper;
using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Services
{
    public class ServicesManager(IUnitOfWorK unitOfWorK,IMapper mapper,IBsaketRepository bsaketRepository, UserManager<ApplicationUser> userManager) : IServicesManager
    {
        private readonly Lazy<IProductServices> _lazyProductServices = new Lazy<IProductServices>(() => new ProductServices(unitOfWorK,mapper));  
        public IProductServices ProductServices => _lazyProductServices.Value;


        private readonly Lazy<IBsaketServices> _lazyBasketServices = new Lazy<IBsaketServices>(() => new BsaketServices(bsaketRepository, mapper));

        public IBsaketServices BasketServices => _lazyBasketServices.Value;

        private readonly Lazy<IAuthenticationServices> _Lazyauthentication = new Lazy<IAuthenticationServices>(() => new AuthenticationServices(userManager));

        public IAuthenticationServices AuthenticationServices => _Lazyauthentication.Value;
    }
}
