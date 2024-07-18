using OMS.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Entities
{
    public class Invoice:BaseEntity
    {
        
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } // Navigation property to Order
    }
}
