using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Entities.Orders
{
    public class OrderItem:BaseEntity
    {
        public OrderItem()
        {
        }

        public OrderItem( decimal price, int quentity)
        {
            
            Price = price;
            Quentity = quentity;
        }

        //public ProductItemOrdered Product { get; set; }
        public decimal Price { get; set; }
        public int Quentity { get; set; }
        public decimal Discount { get; set; }
       

        public int OrderId { get; set; } // Foreign key to Order
        public int ProductId { get; set; } // Foreign key to Product
        public Order Order { get; set; } // Navigation property to Order
        public Product Products { get; set; } // Navigation property to Product
    }
}
