using DNATestingSystem.Repository.NhanVT.Models;
using DNATestingSystem.Services.NhanVT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DNATestingSystem.APIServices.BE.NhanVT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserServiceNhanVtController : ControllerBase
    {
        private readonly IUserServiceNhanVtService _userServiceNhanVtService;

        public UserServiceNhanVtController(IUserServiceNhanVtService userServiceNhanVtService)
        {
            _userServiceNhanVtService = userServiceNhanVtService;
        }

        // GET api/UserServiceNhanVt
        [HttpGet]
        [Authorize(Roles = "1,2,3,4")]
        public async Task<ActionResult<List<UserServiceNhanVt>>> GetAll()
        {
            var results = await _userServiceNhanVtService.GetAllAsync();
            return Ok(results);
        }

        // GET api/UserServiceNhanVt/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "1,2,3,4")]
        public async Task<ActionResult<UserServiceNhanVt>> GetById(int id)
        {
            var result = await _userServiceNhanVtService.GetByIdAsync(id);
            if (result?.UserServiceNhanVtid == 0)
                return NotFound();
            return Ok(result);
        }

        // GET api/UserServiceNhanVt/user/{userId}
        [HttpGet("user/{userId}")]
        [Authorize(Roles = "1,2,3,4")]
        public async Task<ActionResult<List<UserServiceNhanVt>>> GetByUserId(int userId)
        {
            var results = await _userServiceNhanVtService.GetByUserIdAsync(userId);
            return Ok(results);
        }

        // GET api/UserServiceNhanVt/service/{serviceId}
        [HttpGet("service/{serviceId}")]
        [Authorize(Roles = "1,2,3,4")]
        public async Task<ActionResult<List<UserServiceNhanVt>>> GetByServiceId(int serviceId)
        {
            var results = await _userServiceNhanVtService.GetByServiceIdAsync(serviceId);
            return Ok(results);
        }

        // GET api/UserServiceNhanVt/role/{role}
        [HttpGet("role/{role}")]
        [Authorize(Roles = "1,2,3,4")]
        public async Task<ActionResult<List<UserServiceNhanVt>>> GetByRole(string role)
        {
            var results = await _userServiceNhanVtService.GetByRoleAsync(role);
            return Ok(results);
        }

        // GET api/UserServiceNhanVt/active
        [HttpGet("active")]
        [Authorize(Roles = "1,2,3,4")]
        public async Task<ActionResult<List<UserServiceNhanVt>>> GetActiveServices()
        {
            var results = await _userServiceNhanVtService.GetActiveServicesAsync();
            return Ok(results);
        }

        // POST api/UserServiceNhanVt
        [HttpPost]
        [Authorize(Roles = "2,3,4")]
        public async Task<ActionResult<int>> Create([FromBody] UserServiceNhanVt entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userServiceNhanVtService.CreateAsync(entity);
            if (result > 0)
                return CreatedAtAction(nameof(GetById), new { id = result }, result);
            return BadRequest("Failed to create user service");
        }

        // PUT api/UserServiceNhanVt/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "2,3,4")]
        public async Task<ActionResult<int>> Update(int id, [FromBody] UserServiceNhanVt entity)
        {
            if (id != entity.UserServiceNhanVtid)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userServiceNhanVtService.UpdateAsync(entity);
            if (result > 0)
                return Ok(result);
            return NotFound("User service not found or failed to update");
        }

        // DELETE api/UserServiceNhanVt/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "3,4")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _userServiceNhanVtService.DeleteAsync(id);
            if (result)
                return Ok(true);
            return NotFound();
        }
    }
}
