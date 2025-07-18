using DNATestingSystem.Repository.NhanVT.ModelExtensions;
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
    public class SampleThinhLcController : ControllerBase
    {
        private readonly ISampleThinhLcService _sampleService;
        private readonly ILogger<SampleThinhLcController> _logger;

        public SampleThinhLcController(
            ISampleThinhLcService sampleService,
            ILogger<SampleThinhLcController> logger)
        {
            _sampleService = sampleService;
            _logger = logger;
        }

        // GET: api/SampleThinhLc
        [HttpGet]
        [Authorize(Roles = "1,2")]
        public async Task<IEnumerable<SampleThinhLc>> Get()
        {
            return await _sampleService.GetAllAsync();
        }

        // GET: api/SampleThinhLc/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SampleThinhLc>> Get(int id)
        {
            var sample = await _sampleService.GetByIdAsync(id);
            if (sample == null)
                return NotFound();
            return sample;
        }

        // POST: api/SampleThinhLc
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] SampleThinhLc value)
        {
            var id = await _sampleService.CreateAsync(value);
            return Ok(id);
        }

        // PUT: api/SampleThinhLc/5
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(int id, [FromBody] SampleThinhLc value)
        {
            if (id != value.SampleTypeThinhLcid)
                return BadRequest("ID mismatch");
            var result = await _sampleService.UpdateAsync(value);
            return Ok(result);
        }

        // DELETE: api/SampleThinhLc/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _sampleService.DeleteAsync(id);
            return Ok(result);
        }

        // GET: api/SampleThinhLc/profile/5
        [HttpGet("profile/{profileId}")]
        public async Task<IEnumerable<SampleThinhLc>> GetByProfileId(int profileId)
        {
            return await _sampleService.GetByProfileIdAsync(profileId);
        }

        // GET: api/SampleThinhLc/sampletype/5
        [HttpGet("sampletype/{sampleTypeId}")]
        public async Task<IEnumerable<SampleThinhLc>> GetBySampleTypeId(int sampleTypeId)
        {
            return await _sampleService.GetBySampleTypeIdAsync(sampleTypeId);
        }

        // GET: api/SampleThinhLc/appointment/5
        [HttpGet("appointment/{appointmentId}")]
        public async Task<IEnumerable<SampleThinhLc>> GetByAppointmentId(int appointmentId)
        {
            return await _sampleService.GetByAppointmentIdAsync(appointmentId);
        }

        // GET: api/SampleThinhLc/processed
        [HttpGet("processed")]
        public async Task<IEnumerable<SampleThinhLc>> GetProcessedSamples()
        {
            return await _sampleService.GetProcessedSamplesAsync();
        }

        // GET: api/SampleThinhLc/search
        [HttpGet("search")]
        public async Task<PaginationResult<List<SampleThinhLc>>> SearchWithPaging(
            [FromQuery] int? profileId,
            [FromQuery] int? sampleTypeId,
            [FromQuery] bool? isProcessed,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            return await _sampleService.SearchWithPagingAsync(profileId, sampleTypeId, isProcessed, page, pageSize);
        }
    }
}