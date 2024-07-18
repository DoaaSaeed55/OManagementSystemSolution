using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMS.Core.Entities;
using OMS.Core.Repositries.Interfaces;
using OMS.Core.Specification.Invoice_Spec;

namespace OManagementSystemSolution.APIs.Controllers
{
    
    public class InvoiceController : BaseApiController
    {
        private readonly IGenericRepositry<Invoice> _invoiceRepo;

        public InvoiceController(IGenericRepositry<Invoice> invoiceRepo)
        {
            _invoiceRepo = invoiceRepo;
        }


        [HttpGet("{invoiceId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Invoice>> GetInvoiceById(int invoiceId)
        {
            var spec = new InvoiceWithOrderSpec(invoiceId);
            var invoice = await _invoiceRepo.GetWithSpecAsync(spec);

            if (invoice == null)
            {
                return NotFound(new { Message = "Invoice not found", StatusCode = 404 });
            }

            return Ok(invoice);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetAllInvoices()
        {
            var invoices = await _invoiceRepo.GetAllAsync();
            return Ok(invoices);
        }
    }
}
