using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Entities.Orders
{
    public class Order:BaseEntity
    {

        public Order()
        {
        }

        public Order( PaymentMethod paymentMethod, ICollection<OrderItem> items, decimal subTotaL)
        {
            
            Items = items;
            SubTotaL = subTotaL;
            PaymentMethod = paymentMethod;
        }

      

        public DateTimeOffset OrderDate { get; set; }= DateTimeOffset.Now;
        public OrderStatus status { get; set; } = OrderStatus.Pending;
        public DeliveryMethod DeliveryMethod { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotaL { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public decimal TotaL { get; set; }
        //public decimal TotaL { get => SubTotaL + DeliveryMethod.Cost; } 
        //  public decimal GetTotaL()  => SubTotaL + DeliveryMethod.Cost;

        public string PaymentIntitId { get; set; }= string.Empty;

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Invoice Invoice { get; set; } // Navigation property to Invoice


    }
}
