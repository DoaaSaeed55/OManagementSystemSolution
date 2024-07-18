using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Entities.Orders
{
    public class PaymentResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string TransactionId { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
