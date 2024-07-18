using OMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Specification.Products_Spec
{
    public class CustomerWithOrderSpec : BaseSpecification<Customer>
    {
        public CustomerWithOrderSpec() : base()
        {
            Include.Add(p => p.Orders);
        }

        public CustomerWithOrderSpec(int id) : base(p => p.Id == id)
        {
            Include.Add(p => p.Orders);
        }
    }
}
