using DNATestingSystem.Repository.NhanVT.Models;
using DNATestingSystem.Services.NhanVT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DNATestingSystem.APIServices.BE.NhanVT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LociPhienNTController : ControllerBase
    {
        private readonly ILociPhienNtService _lociPhienNtService;

        public LociPhienNTController(ILociPhienNtService lociPhienNtService)
        {
            _lociPhienNtService = lociPhienNtService;
        }


        // GET api/LociPhienNT
        [HttpGet]
        [Authorize(Roles = "1,2,3,4")]
        public async Task<ActionResult<List<LociPhienNt>>> GetAll()
        {
            var loci = await _lociPhienNtService.GetAllLociAsync();
            return Ok(loci);
        }

        // GET api/LociPhienNT/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "1,2,3,4")]
        public async Task<ActionResult<LociPhienNt>> GetById(int id)
        {
            var locus = await _lociPhienNtService.GetLocusByIdAsync(id);
            if (locus?.PhienNtid == 0)
                return NotFound();
            return Ok(locus);
        }

        // GET api/LociPhienNT/name/{name}
        [HttpGet("name/{name}")]
        [Authorize(Roles = "1,2,3,4")]
        public async Task<ActionResult<LociPhienNt>> GetByName(string name)
        {
            var locus = await _lociPhienNtService.GetLocusByNameAsync(name);
            if (locus?.PhienNtid == 0)
                return NotFound();
            return Ok(locus);
        }

        // GET api/LociPhienNT/codis
        [HttpGet("codis")]
        [Authorize(Roles = "1,2,3,4")]
        public async Task<ActionResult<List<LociPhienNt>>> GetCodisLoci()
        {
            var loci = await _lociPhienNtService.GetCodisLociAsync();
            return Ok(loci);
        }


        // POST api/LociPhienNT
        [HttpPost]
        [Authorize(Roles = "3,4")]
        public async Task<ActionResult<int>> Create([FromBody] LociPhienNt entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _lociPhienNtService.CreateLocusAsync(entity);
            if (result > 0)
                return CreatedAtAction(nameof(GetById), new { id = result }, result);
            return BadRequest("Failed to create locus");
        }

        // PUT api/LociPhienNT/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "3,4")]
        public async Task<ActionResult<int>> Update(int id, [FromBody] LociPhienNt entity)
        {
            if (id != entity.PhienNtid)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _lociPhienNtService.UpdateLocusAsync(entity);
            if (result > 0)
                return Ok(result);
            return NotFound("Locus not found or failed to update");
        }

        // DELETE api/LociPhienNT/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "4")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _lociPhienNtService.DeleteLocusAsync(id);
            if (result)
                return Ok(true);
            return NotFound();
        }
    }
}
