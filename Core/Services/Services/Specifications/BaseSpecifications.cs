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
        #region Criteria

        public BaseSpecifications(Expression<Func<Tentity, bool>>? PassedExpression)
        {
            Criteria = PassedExpression;
        }

        public Expression<Func<Tentity, bool>>? Criteria { get; private set; } 
        #endregion

        #region Includes
        public List<Expression<Func<Tentity, object>>> IncludeExpressions { get; } = new List<Expression<Func<Tentity, object>>>();

       

        protected void AddInclude(Expression<Func<Tentity, object>> includeExp)
        {
            IncludeExpressions.Add(includeExp);
        }
        #endregion

        #region Sorting

        public Expression<Func<Tentity, object>> OrderBy { get; private set; }

        public Expression<Func<Tentity, object>> OrderByDesc { get; private set; }

      


        protected void AddOrderBy (Expression<Func<Tentity, object>> ordderByExpression)=> OrderBy = ordderByExpression;
        protected void AddOrderByDesc (Expression<Func<Tentity, object>> ordderByDescExpression)=> OrderByDesc = ordderByDescExpression;
        #endregion

        #region Pagination
        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get; set ; }

        protected void ApllyPagination (int PageSize, int PageIndex)
        {
            IsPaginated = true;
            Take = PageSize;
            Skip = (PageIndex-1)*PageSize;
        }

        #endregion
    }
}