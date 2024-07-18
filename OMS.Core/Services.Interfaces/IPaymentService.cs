using OMS.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Services.Interfaces
{
    public interface IPaymentService
    {
       // Task ProcessPayment(Order order);
        Task<PaymentResult> ProcessPayment(Order order);

        // CreateOrUpdatePaymentIntent(String)
    }
}
