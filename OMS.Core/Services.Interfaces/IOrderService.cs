using OMS.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Services.Interfaces
{
    public interface IOrderService
    {

        //Task<Order?> CreateOrderAsync(string BuyerEmail,int DeliveryMethod);
        //Task<IReadOnlyList<Order?>> GetOrdersForSpecificUserAsync(string BuyerEmail);
        //Task<IReadOnlyList<Order?>> GetOrderByIdForSpecificUserAsync(string BuyerEmail,int OrderId);

        Task CreateOrderAsync(Order order);
        Task<IEnumerable<Order>> GetOrdersForCustomerAsync(int customerId);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task UpdateOrderStatusAsync(int orderId, OrderStatus status);

    }
}
