using OMS.Core.Entities.Orders;
using OMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Services.Interfaces
{
    public interface ICustomerService
    {

        Task<Customer?> CreateCustomerAsync(Customer customer);
        Task<Customer?> GetCustomerByIdAsync(int customerId);
        Task<IEnumerable<Order>> GetOrdersForCustomerAsync(int customerId);
    }
}
