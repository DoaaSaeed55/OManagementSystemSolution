using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMS.Core.Entities.Orders;
using OMS.Core.Services.Interfaces;

namespace OManagementSystemSolution.APIs.Controllers
{

    public class OrderController : BaseApiController
    {

        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            try
            {
                await _orderService.CreateOrderAsync(order);
                return Ok(new { message = "Order created successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        [Route("customer/{customerId}")]
        public async Task<IActionResult> GetOrdersForCustomer(int customerId)
        {
            var orders = await _orderService.GetOrdersForCustomerAsync(customerId);
            if (orders == null || !orders.Any())
            {
                return NotFound(new { message = "No orders found for this customer." });
            }
            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound(new { message = "Order not found." });
            }
            return Ok(order);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            if (orders == null || !orders.Any())
            {
                return NotFound(new { message = "No orders found." });
            }
            return Ok(orders);
        }

        [HttpPut("{orderId}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] OrderStatus status)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(orderId);
                if (order == null)
                {
                    return NotFound(new { message = "Order not found." });
                }
                await _orderService.UpdateOrderStatusAsync(orderId, status);
                return Ok(new { message = "Order status updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
