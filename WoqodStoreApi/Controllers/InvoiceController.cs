using Microsoft.AspNetCore.Mvc;
using WoqodData.Models;
using WoqodData.Repository;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WoqodStoreBL.BusinessLayer;

namespace WoqodStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IStoreInvoicesRepository _invoiceRepository;
        private readonly ILogger<InvoiceController> _logger;


        public InvoiceController(IStoreInvoicesRepository invoiceRepository, ILogger<InvoiceController> logger)
        {
            _invoiceRepository = invoiceRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get(long receiptNo, int storeId, DateTime fromDate, DateTime toDate, int pageSize = 10, int pageNumber = 1)
        {
            InvoiceBL _invoiceBL =new InvoiceBL();
            var validationError = _invoiceBL.ValidateRequestParameters(receiptNo, storeId, fromDate, toDate);
            if (validationError != null)
            {
                _logger.LogWarning("Validation failed. Error: {ValidationError}", validationError);
                return BadRequest(validationError);
            }

            try
            {
                _logger.LogInformation("Fetching invoices from repository.");
                var retrievedInvoices = await _invoiceRepository.GetInvoices(receiptNo, storeId, fromDate, toDate, pageSize, pageNumber);

                if (retrievedInvoices == null || !retrievedInvoices.Any())
                {
                    _logger.LogInformation("No invoices found for the given criteria.");
                    return Ok(new { message = "No invoices found for the given criteria.", data = new List<StoreInvoices>() });
                }
                _logger.LogInformation("Invoices retrieved successfully.");
                return Ok(retrievedInvoices); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching invoices.");
                return StatusCode(500, new { message = $"An error occurred while fetching invoices: {ex.Message}" });
            }
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var retrievedInvoice = await _invoiceRepository.GetInvoiceById(id);

                if (retrievedInvoice == null)
                {
                    return NotFound($"Invoice with ID {id} was not found.");
                }

                return Ok(retrievedInvoice); // Return 200 status with the invoice data
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message =  $"An error occurred while fetching the invoice: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] StoreInvoices updatedInvoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data format. Please check your input." });
            }

            try
            {
                var existingInvoice = await _invoiceRepository.GetInvoiceById(id);

                if (existingInvoice == null)
                {
                    return NotFound(new { message = $"Invoice with ID {id} does not exist." });
                }

                updatedInvoice.Id = id;

                var result = await _invoiceRepository.UpdateInvoice(updatedInvoice);

                if (!result)
                {
                    return BadRequest(new { message = "Failed to update the invoice." });
                }

                return Ok(new { message = "Invoice updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while updating the invoice: {ex.Message}" });
            }
        }

    }

    
}
