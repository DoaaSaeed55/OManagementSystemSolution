using OMS.Core;
using OMS.Core.Entities;
using OMS.Core.Entities.Orders;
using OMS.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Servise
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }


        public async Task<Customer?> CreateCustomerAsync(Customer customer)
        {
            
            await _unitOfWork.Repositry<Customer>().AddAsync(customer);
            return customer;
        }

        public async Task<Customer?> GetCustomerByIdAsync(int customerId)
        {
            return await _unitOfWork.Repositry<Customer>().GetAsync(customerId);
        }

        public async Task<IEnumerable<Order>> GetOrdersForCustomerAsync(int customerId)
        {
            var orders = (await _unitOfWork.Repositry<Order>().GetAllAsync()).Where(o => o.CustomerId == customerId);
            return orders;
        }
    }
}
