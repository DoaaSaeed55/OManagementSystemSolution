using OMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Specification.Products_Spec
{
    public class ProductwithOrderSpec:BaseSpecification<Product>
    {

        public ProductwithOrderSpec() : base()
        {
            Include.Add(p => p.OrderItems);
        }

        public ProductwithOrderSpec(int id) : base(p => p.Id == id)
        {
            Include.Add(p => p.OrderItems);
        }
    }
}
