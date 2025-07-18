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
    public class ProfileThinhLcController : ControllerBase
    {
        private readonly IProfileThinhLcService _service;
        private readonly ILogger<ProfileThinhLcController> _logger;

        public ProfileThinhLcController(
            IProfileThinhLcService service,
            ILogger<ProfileThinhLcController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: api/ProfileThinhLc
        [HttpGet]
        [Authorize(Roles = "1,2")]
        public async Task<ActionResult<IEnumerable<ProfileThinhLc>>> Get()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // GET: api/ProfileThinhLc/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileThinhLc>> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // POST: api/ProfileThinhLc
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] ProfileThinhLc value)
        {
            var id = await _service.CreateAsync(value);
            return Ok(id);
        }

        // PUT: api/ProfileThinhLc/5
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(int id, [FromBody] ProfileThinhLc value)
        {
            if (id != value.ProfileThinhLcid)
                return BadRequest("ID mismatch");
            var result = await _service.UpdateAsync(value);
            return Ok(result);
        }

        // DELETE: api/ProfileThinhLc/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return Ok(result);
        }
    }
}