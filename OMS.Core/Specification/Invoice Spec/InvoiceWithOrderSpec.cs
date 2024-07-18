using OMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Specification.Invoice_Spec
{
    public class InvoiceWithOrderSpec : BaseSpecification<Invoice>
    {

        public InvoiceWithOrderSpec() : base()
        {
            Include.Add(p => p.Order);
        }

        public InvoiceWithOrderSpec(int id) : base(p => p.Id == id)
        {
            Include.Add(p => p.Order);
        }
    }
}
