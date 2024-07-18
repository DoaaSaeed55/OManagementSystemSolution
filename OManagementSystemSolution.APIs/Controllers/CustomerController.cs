using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMS.Core;
using OMS.Core.Entities;
using OMS.Core.Entities.Orders;
using OMS.Core.Repositries.Interfaces;
using OMS.Core.Services.Interfaces;
using OMS.Core.Specification.Products_Spec;

namespace OManagementSystemSolution.APIs.Controllers
{

    public class CustomerController : BaseApiController
    {
        

        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(Customer customer)
        {
            try
            {
                var createdCustomer = await _customerService.CreateCustomerAsync(customer);
                return Ok(new { message = "Customer created successfully.", CustomerId = createdCustomer?.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerById(int customerId)
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                return NotFound(new { message = "Customer not found." });
            }
            return Ok(customer);
        }

        [HttpGet("{customerId}/orders")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> GetOrdersForCustomer(int customerId)
        {
            var orders = await _customerService.GetOrdersForCustomerAsync(customerId);
            return Ok(orders);
        }
    } 

}
  
