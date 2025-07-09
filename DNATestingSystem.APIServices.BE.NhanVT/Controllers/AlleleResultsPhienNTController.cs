using DNATestingSystem.Repository.NhanVT.Models;
using DNATestingSystem.Services.NhanVT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DNATestingSystem.APIServices.BE.NhanVT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlleleResultsPhienNTController : ControllerBase
    {
        private readonly IAlleleResultsPhienNtService _alleleResultsPhienNtService;

        public AlleleResultsPhienNTController(IAlleleResultsPhienNtService alleleResultsPhienNtService)
        {
            _alleleResultsPhienNtService = alleleResultsPhienNtService;
        }

        // GET api/AlleleResultsPhienNT
        [HttpGet]
        [Authorize(Roles = "1,2,3,4")]
        public async Task<ActionResult<List<AlleleResultsPhienNt>>> GetAll()
        {
            var results = await _alleleResultsPhienNtService.GetAllAsync();
            return Ok(results);
        }

        // GET api/AlleleResultsPhienNT/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "1,2,3,4")]
        public async Task<ActionResult<AlleleResultsPhienNt>> GetById(int id)
        {
            var result = await _alleleResultsPhienNtService.GetByIdAsync(id);
            if (result?.PhienNtid == 0)
                return NotFound();
            return Ok(result);
        }

        // GET api/AlleleResultsPhienNT/test/{testId}
        [HttpGet("test/{testId}")]
        [Authorize(Roles = "1,2,3,4")]
        public async Task<ActionResult<List<AlleleResultsPhienNt>>> GetByTestId(int testId)
        {
            var results = await _alleleResultsPhienNtService.GetByTestIdAsync(testId);
            return Ok(results);
        }

        // GET api/AlleleResultsPhienNT/profile/{profileId}
        [HttpGet("profile/{profileId}")]
        [Authorize(Roles = "1,2,3,4")]
        public async Task<ActionResult<List<AlleleResultsPhienNt>>> GetByProfileId(int profileId)
        {
            var results = await _alleleResultsPhienNtService.GetByProfileIdAsync(profileId);
            return Ok(results);
        }

        // GET api/AlleleResultsPhienNT/locus/{locusId}
        [HttpGet("locus/{locusId}")]
        [Authorize(Roles = "1,2,3,4")]
        public async Task<ActionResult<List<AlleleResultsPhienNt>>> GetByLocusId(int locusId)
        {
            var results = await _alleleResultsPhienNtService.GetByLocusIdAsync(locusId);
            return Ok(results);
        }

        // POST api/AlleleResultsPhienNT
        [HttpPost]
        [Authorize(Roles = "2,3,4")]
        public async Task<ActionResult<int>> Create([FromBody] AlleleResultsPhienNt entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _alleleResultsPhienNtService.CreateAsync(entity);
            if (result > 0)
                return CreatedAtAction(nameof(GetById), new { id = result }, result);
            return BadRequest("Failed to create allele result");
        }

        // PUT api/AlleleResultsPhienNT/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "2,3,4")]
        public async Task<ActionResult<int>> Update(int id, [FromBody] AlleleResultsPhienNt entity)
        {
            if (id != entity.PhienNtid)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _alleleResultsPhienNtService.UpdateAsync(entity);
            if (result > 0)
                return Ok(result);
            return NotFound("Allele result not found or failed to update");
        }

        // DELETE api/AlleleResultsPhienNT/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "3,4")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _alleleResultsPhienNtService.DeleteAsync(id);
            if (result)
                return Ok(true);
            return NotFound();
        }
    }
}
