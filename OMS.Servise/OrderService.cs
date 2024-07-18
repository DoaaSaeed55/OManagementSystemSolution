using OMS.Core;
using OMS.Core.Entities;
using OMS.Core.Entities.Orders;
using OMS.Core.Repositries.Interfaces;
using OMS.Core.Services.Interfaces;
using OMS.Repositry.Repostries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Servise
{
    public class OrderService : IOrderService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;
        private readonly IEmailService _emailService;

        public OrderService(IUnitOfWork unitOfWork, IPaymentService paymentService, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
            _emailService = emailService;
        }
        public async Task CreateOrderAsync(Order order)
        {
            // Validate order
            foreach (var item in order.Items)
            {
                var product = await _unitOfWork.Repositry<Product>().GetAsync(item.ProductId);
                if (product.Stock < item.Quentity)
                {
                    throw new Exception("Insufficient stock for product " + product.Name);
                }
            }

            // Apply discounts
            if (order.TotaL > 200)
            {
                order.TotaL *= 0.90m; // 10% discount
            }
            else if (order.TotaL > 100)
            {
                order.TotaL *= 0.95m; // 5% discount
            }

            // Update stock
            foreach (var item in order.Items)
            {
                var product = await _unitOfWork.Repositry<Product>().GetAsync(item.ProductId);
                product.Stock -= item.Quentity;
                _unitOfWork.Repositry<Product>().UpdateAsync(product);
            }

            // Process payment
            await _paymentService.ProcessPayment(order);

            // Save order
            await _unitOfWork.Repositry<Order>().AddAsync(order);

            // Generate invoice
            var invoice = new Invoice
            {
                OrderId = order.Id,
                InvoiceDate = DateTime.Now,
                TotalAmount = order.TotaL
            };
            await _unitOfWork.Repositry<Invoice>().AddAsync(invoice);

            // Send email notification
            var customer = await _unitOfWork.Repositry<Customer>().GetAsync(order.CustomerId);
            if (customer != null)
            {
                var emailSubject = "Your order has been placed";
                var emailBody = $"Dear {customer.Name},<br><br>Your order with ID {order.Id} has been placed successfully. The total amount is {order.TotaL}.<br><br>Thank you for shopping with us.";
                await _emailService.SendEmailAsync(customer.Email, emailSubject, emailBody);
            }

        }

        public async Task<IEnumerable<Order>> GetOrdersForCustomerAsync(int customerId) =>
            (await _unitOfWork.Repositry<Order>().GetAllAsync()).Where(o => o.CustomerId == customerId);

        public async Task<Order> GetOrderByIdAsync(int orderId) =>
            await _unitOfWork.Repositry<Order>().GetAsync(orderId);

        public async Task<IEnumerable<Order>> GetAllOrdersAsync() =>
            await _unitOfWork.Repositry<Order>().GetAllAsync();

        public async Task UpdateOrderStatusAsync(int orderId, OrderStatus status)
        {
            var order = await _unitOfWork.Repositry<Order>().GetAsync(orderId);
            if (order != null)
            {
                order.status = status;
                _unitOfWork.Repositry<Order>().UpdateAsync(order);

                // Send email notification (implementation not shown)
            }
        }
    }
}
