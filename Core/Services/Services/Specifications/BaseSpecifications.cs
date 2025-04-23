using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;

namespace Services.Specifications
{
    public abstract class BaseSpecifications<Tentity, TKey> : ISpecifications<Tentity, TKey>
        where Tentity : ModelBase<TKey>
    {
        public BaseSpecifications(Expression<Func<Tentity, bool>>? PassedExpression)
        {
            Criteria = PassedExpression;
        }

        public Expression<Func<Tentity, bool>>? Criteria { get; private set; }

        public List<Expression<Func<Tentity, object>>> IncludeExpressions { get; } = new List<Expression<Func<Tentity, object>>>();



        protected void AddInclude(Expression<Func<Tentity, object>> includeExp)
        {
            IncludeExpressions.Add(includeExp);
        }
    }
}