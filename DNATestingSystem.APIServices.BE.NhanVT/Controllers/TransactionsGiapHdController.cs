using DNATestingSystem.Repository.NhanVT.Models;
using DNATestingSystem.Services.NhanVT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DNATestingSystem.APIServices.BE.NhanVT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsGiapHdController : ControllerBase
    {
        private readonly ITransactionsGiapHdService _transactionsGiapHdService;

        public TransactionsGiapHdController(ITransactionsGiapHdService transactionsGiapHdService)
        {
            _transactionsGiapHdService = transactionsGiapHdService;
        }

        // GET api/TransactionsGiapHd
        [HttpGet]
        [Authorize(Roles = "1,2,3,4")]
        public async Task<ActionResult<List<TransactionsGiapHd>>> GetAll()
        {
            var results = await _transactionsGiapHdService.GetAllAsync();
            return Ok(results);
        }

        // GET api/TransactionsGiapHd/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "1,2,3,4")]
        public async Task<ActionResult<TransactionsGiapHd>> GetById(int id)
        {
            var result = await _transactionsGiapHdService.GetByIdAsync(id);
            if (result?.TransactionsGiapHdid == 0)
                return NotFound();
            return Ok(result);
        }

        // GET api/TransactionsGiapHd/order/{orderId}
        [HttpGet("order/{orderId}")]
        [Authorize(Roles = "1,2,3,4")]
        public async Task<ActionResult<List<TransactionsGiapHd>>> GetByOrderId(int orderId)
        {
            var results = await _transactionsGiapHdService.GetByOrderIdAsync(orderId);
            return Ok(results);
        }

        // GET api/TransactionsGiapHd/payment-method/{paymentMethod}
        [HttpGet("payment-method/{paymentMethod}")]
        [Authorize(Roles = "1,2,3,4")]
        public async Task<ActionResult<List<TransactionsGiapHd>>> GetByPaymentMethod(string paymentMethod)
        {
            var results = await _transactionsGiapHdService.GetByPaymentMethodAsync(paymentMethod);
            return Ok(results);
        }

        // GET api/TransactionsGiapHd/status/{status}
        [HttpGet("status/{status}")]
        [Authorize(Roles = "1,2,3,4")]
        public async Task<ActionResult<List<TransactionsGiapHd>>> GetByStatus(string status)
        {
            var results = await _transactionsGiapHdService.GetByStatusAsync(status);
            return Ok(results);
        }

        // POST api/TransactionsGiapHd
        [HttpPost]
        [Authorize(Roles = "2,3,4")]
        public async Task<ActionResult<int>> Create([FromBody] TransactionsGiapHd entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _transactionsGiapHdService.CreateAsync(entity);
            if (result > 0)
                return CreatedAtAction(nameof(GetById), new { id = result }, result);
            return BadRequest("Failed to create transaction");
        }

        // PUT api/TransactionsGiapHd/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "2,3,4")]
        public async Task<ActionResult<int>> Update(int id, [FromBody] TransactionsGiapHd entity)
        {
            if (id != entity.TransactionsGiapHdid)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _transactionsGiapHdService.UpdateAsync(entity);
            if (result > 0)
                return Ok(result);
            return NotFound("Transaction not found or failed to update");
        }

        // DELETE api/TransactionsGiapHd/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "3,4")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _transactionsGiapHdService.DeleteAsync(id);
            if (result)
                return Ok(true);
            return NotFound();
        }
    }
}
