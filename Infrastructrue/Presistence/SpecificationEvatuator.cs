using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Presistence
{
    public static class SpecificationEvatuator
    {

        //InPut Query 
        // context.Set<TEntity>()=> input query parameter

        public static IQueryable<Tentity> CreateQuery<Tentity, TKey>(IQueryable<Tentity> InputQuery, ISpecifications<Tentity, TKey> spec)
            where Tentity : ModelBase<TKey>
        {
            var query = InputQuery;
            if (spec.Criteria is not null)
              query = query.Where(spec.Criteria); 
            

            if (spec.IncludeExpressions is not null && spec.IncludeExpressions.Count>0)
             query = spec.IncludeExpressions.Aggregate(query, (CurrentQuery, Exp) => CurrentQuery.Include(Exp));
            

            return query;
        }

    }
}
  