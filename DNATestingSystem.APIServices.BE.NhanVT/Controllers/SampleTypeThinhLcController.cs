using DNATestingSystem.Repository.NhanVT.Models;
using DNATestingSystem.Services.NhanVT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DNATestingSystem.APIServices.BE.NhanVT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleTypeThinhLcController : ControllerBase
    {
        private readonly ISampleTypeThinhLcService _service;
        private readonly ILogger<SampleTypeThinhLcController> _logger;

        public SampleTypeThinhLcController(
            ISampleTypeThinhLcService service,
            ILogger<SampleTypeThinhLcController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: api/SampleTypeThinhLc
        [HttpGet]
        [Authorize(Roles = "1,2")]
        public async Task<ActionResult<IEnumerable<SampleTypeThinhLc>>> Get()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // GET: api/SampleTypeThinhLc/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SampleTypeThinhLc>> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // GET: api/SampleTypeThinhLc/typeName/{typeName}
        [HttpGet("typeName/{typeName}")]
        public async Task<ActionResult<SampleTypeThinhLc>> GetByTypeName(string typeName)
        {
            var result = await _service.GetByTypeNameAsync(typeName);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // GET: api/SampleTypeThinhLc/active
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<SampleTypeThinhLc>>> GetActive()
        {
            var result = await _service.GetActiveAsync();
            return Ok(result);
        }

        // POST: api/SampleTypeThinhLc
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] SampleTypeThinhLc value)
        {
            var id = await _service.CreateAsync(value);
            return Ok(id);
        }

        // PUT: api/SampleTypeThinhLc/5
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(int id, [FromBody] SampleTypeThinhLc value)
        {
            if (id != value.SampleTypeThinhLcid)
                return BadRequest("ID mismatch");
            var result = await _service.UpdateAsync(value);
            return Ok(result);
        }

        // DELETE: api/SampleTypeThinhLc/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return Ok(result);
        }

        // PUT: api/SampleTypeThinhLc/activate/5
        [HttpPut("activate/{id}")]
        public async Task<ActionResult<bool>> Activate(int id)
        {
            var result = await _service.ActivateAsync(id);
            return Ok(result);
        }

        // PUT: api/SampleTypeThinhLc/deactivate/5
        [HttpPut("deactivate/{id}")]
        public async Task<ActionResult<bool>> Deactivate(int id)
        {
            var result = await _service.DeactivateAsync(id);
            return Ok(result);
        }
    }
}