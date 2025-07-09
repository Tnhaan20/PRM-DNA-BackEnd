using DNATestingSystem.Repository.NhanVT.Models;
using DNATestingSystem.Services.NhanVT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DNATestingSystem.APIServices.BE.NhanVT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCategoriesHuyLhgController : ControllerBase
    {
        private readonly IBlogCategoriesHuyLhgService _blogCategoriesHuyLhgService;

        public BlogCategoriesHuyLhgController(IBlogCategoriesHuyLhgService blogCategoriesHuyLhgService)
        {
            _blogCategoriesHuyLhgService = blogCategoriesHuyLhgService;
        }

        // GET api/BlogCategoriesHuyLhg
        [HttpGet]
        [Authorize(Roles = "1,2,3,4")]
        public async Task<ActionResult<List<BlogCategoriesHuyLhg>>> GetAll()
        {
            var results = await _blogCategoriesHuyLhgService.GetAllAsync();
            return Ok(results);
        }

        // GET api/BlogCategoriesHuyLhg/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "1,2,3,4")]
        public async Task<ActionResult<BlogCategoriesHuyLhg>> GetById(int id)
        {
            var result = await _blogCategoriesHuyLhgService.GetByIdAsync(id);
            if (result?.BlogCategoryHuyLhgid == 0)
                return NotFound();
            return Ok(result);
        }

        // GET api/BlogCategoriesHuyLhg/active
        [HttpGet("active")]
        [Authorize(Roles = "1,2,3,4")]
        public async Task<ActionResult<List<BlogCategoriesHuyLhg>>> GetActiveCategories()
        {
            var results = await _blogCategoriesHuyLhgService.GetActiveCategoriesAsync();
            return Ok(results);
        }

        // POST api/BlogCategoriesHuyLhg
        [HttpPost]
        [Authorize(Roles = "2,3,4")]
        public async Task<ActionResult<int>> Create([FromBody] BlogCategoriesHuyLhg entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _blogCategoriesHuyLhgService.CreateAsync(entity);
            if (result > 0)
                return CreatedAtAction(nameof(GetById), new { id = result }, result);
            return BadRequest("Failed to create blog category");
        }

        // PUT api/BlogCategoriesHuyLhg/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "2,3,4")]
        public async Task<ActionResult<int>> Update(int id, [FromBody] BlogCategoriesHuyLhg entity)
        {
            if (id != entity.BlogCategoryHuyLhgid)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _blogCategoriesHuyLhgService.UpdateAsync(entity);
            if (result > 0)
                return Ok(result);
            return NotFound("Blog category not found or failed to update");
        }

        // DELETE api/BlogCategoriesHuyLhg/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "3,4")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _blogCategoriesHuyLhgService.DeleteAsync(id);
            if (result)
                return Ok(true);
            return NotFound();
        }
    }
}
