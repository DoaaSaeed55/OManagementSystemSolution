using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Entities.Orders
{
    public class DeliveryMethod:BaseEntity
    {
        public DeliveryMethod()
        {
        }

        public DeliveryMethod(string shortName, string discreption, string deliveryTime, decimal cost)
        {
            ShortName = shortName;
            Discreption = discreption;
            DeliveryTime = deliveryTime;
            Cost = cost;
        }

        public string ShortName { get; set; }
        public string Discreption { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Cost { get; set; }
    }
}
