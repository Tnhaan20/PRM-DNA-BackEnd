using DNATestingSystem.Repository.NhanVT.Models;
using DNATestingSystem.Services.NhanVT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DNATestingSystem.APIServices.BE.NhanVT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DnaTestsPhienNTController : ControllerBase
    {
        private readonly IDnaTestsPhienNtService _dnaTestsPhienNtService;

        public DnaTestsPhienNTController(IDnaTestsPhienNtService dnaTestsPhienNtService)
        {
            _dnaTestsPhienNtService = dnaTestsPhienNtService;
        }


        // GET api/DnaTestsPhienNT
        [HttpGet]
        [Authorize(Roles = "1,2,3,4")]
        public async Task<ActionResult<List<DnaTestsPhienNt>>> GetAll()
        {
            var tests = await _dnaTestsPhienNtService.GetAllDnaTestsAsync();
            return Ok(tests);
        }

        // GET api/DnaTestsPhienNT/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "1,2,3,4")]
        public async Task<ActionResult<DnaTestsPhienNt>> GetById(int id)
        {
            var test = await _dnaTestsPhienNtService.GetDnaTestByIdAsync(id);
            if (test?.PhienNtid == 0)
                return NotFound();
            return Ok(test);
        }

        // GET api/DnaTestsPhienNT/type/{testType}
        [HttpGet("type/{testType}")]
        [Authorize(Roles = "1,2,3,4")]
        public async Task<ActionResult<List<DnaTestsPhienNt>>> GetByTestType(string testType)
        {
            var tests = await _dnaTestsPhienNtService.GetDnaTestsByTypeAsync(testType);
            return Ok(tests);
        }

        // GET api/DnaTestsPhienNT/completed
        [HttpGet("completed")]
        [Authorize(Roles = "1,2,3,4")]
        public async Task<ActionResult<List<DnaTestsPhienNt>>> GetCompleted()
        {
            var tests = await _dnaTestsPhienNtService.GetCompletedDnaTestsAsync();
            return Ok(tests);
        }

        

        // POST api/DnaTestsPhienNT
        [HttpPost]
        [Authorize(Roles = "2,3,4")]
        public async Task<ActionResult<int>> Create([FromBody] DnaTestsPhienNt entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _dnaTestsPhienNtService.CreateDnaTestAsync(entity);
            if (result > 0)
                return CreatedAtAction(nameof(GetById), new { id = result }, result);
            return BadRequest("Failed to create DNA test");
        }

        // PUT api/DnaTestsPhienNT/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "2,3,4")]
        public async Task<ActionResult<int>> Update(int id, [FromBody] DnaTestsPhienNt entity)
        {
            if (id != entity.PhienNtid)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _dnaTestsPhienNtService.UpdateDnaTestAsync(entity);
            if (result > 0)
                return Ok(result);
            return NotFound("DNA test not found or failed to update");
        }

        // DELETE api/DnaTestsPhienNT/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "3,4")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _dnaTestsPhienNtService.DeleteDnaTestAsync(id);
            if (result)
                return Ok(true);
            return NotFound();
        }

    }
}
