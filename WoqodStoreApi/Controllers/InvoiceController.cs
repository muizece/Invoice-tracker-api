using Microsoft.AspNetCore.Mvc;
using WoqodData.Models;
using WoqodData.Repository;
using System.Threading.Tasks;

namespace WoqodStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IStoreInvoicesRepository _invoiceRepository;

        public InvoiceController(IStoreInvoicesRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(long receiptNo, int storeId, DateTime fromDate, DateTime toDate)
        {
            var validationError = ValidateRequestParameters(receiptNo, storeId, fromDate, toDate);
            if (validationError != null)
            {
                return BadRequest(validationError);
            }

            try
            {
                var retrievedInvoices = await _invoiceRepository.GetInvoices(receiptNo, storeId, fromDate, toDate);

                if (retrievedInvoices == null || !retrievedInvoices.Any())
                {
                    return NotFound("No invoices found for the given criteria.");
                }

                return Ok(retrievedInvoices); // Return 200 status with the invoice data
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching invoices: {ex.Message}");
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
                return StatusCode(500, $"An error occurred while fetching the invoice: {ex.Message}");
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

        private string ValidateRequestParameters(long receiptNo, int storeId, DateTime fromDate, DateTime toDate)
        {
            if (fromDate > toDate)
            {
                return "Invalid date range. 'From Date' cannot be greater than 'To Date'.";
            }
            if (storeId <= 0)
            {
                return "Please provide a valid storeId.";
            }
            if (receiptNo <= 0)
            {
                return "Please provide a valid receipt number.";
            }

            return null; // All parameters are valid
        }
    }
}
