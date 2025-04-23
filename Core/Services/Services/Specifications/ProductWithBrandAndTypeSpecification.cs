using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Products;

namespace Services.Specifications
{
    public class ProductWithBrandAndTypeSpecification:BaseSpecifications<Product,int>
    {
        public ProductWithBrandAndTypeSpecification():base(null)
        {
            AddInclude(p=>p.Brand);
            AddInclude(p=>p.Type);
        }


    }
}
