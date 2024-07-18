using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMS.Core.Entities;
using OMS.Core.Repositries.Interfaces;
using OMS.Core.Specification;
using OMS.Core.Specification.Products_Spec;

namespace OManagementSystemSolution.APIs.Controllers
{
   
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepositry<Product> _productRepo;

        public ProductsController(IGenericRepositry<Product> productRepo) 
        {
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            //var products=await _productRepo.GetAllAsync();
            var spec = new ProductwithOrderSpec();
            var products = await _productRepo.GetAllWithSpecAsync(spec);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var spec = new ProductwithOrderSpec(id);
            var product = await _productRepo.GetWithSpecAsync(spec);
            if (product == null)
            {
                return NotFound(new { Message = "Not Found", StatusCode = 404 }); //404
            }

            return Ok(product); //200
        }

        [HttpPost]
         [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            // Validate product input
            if (string.IsNullOrEmpty(product.Name) || product.Price <= 0 || product.Stock < 0)
            {
                return BadRequest("Invalid product data.");
            }
            await _productRepo.AddAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            await _productRepo.UpdateAsync(product);
            return NoContent();
        }

    }
}
