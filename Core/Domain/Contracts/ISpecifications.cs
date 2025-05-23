﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface ISpecifications<Tentity,TKey> where Tentity : ModelBase<TKey>
    {
        public Expression<Func<Tentity,bool>>? Criteria {  get; }

        List<Expression<Func<Tentity,object>>> IncludeExpressions { get; }
    }
}
