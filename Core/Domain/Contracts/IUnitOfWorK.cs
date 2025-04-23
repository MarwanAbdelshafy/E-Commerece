using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Products;

namespace Domain.Contracts
{
    public interface IUnitOfWorK
    {
        //IGenericRepository<Product, int> ProductRepo { get; }
        Task<int> SaveChangesAsync();

        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity :ModelBase<TKey> ;
    }
}
