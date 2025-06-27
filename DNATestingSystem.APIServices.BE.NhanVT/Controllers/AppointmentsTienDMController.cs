using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using DNATestingSystem.Repository.NhanVT.Models;
using DNATestingSystem.Repository.TienDM.ModelExtensions;
using DNATestingSystem.Services.NhanVT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SearchAppointmentsTienDm = DNATestingSystem.Repository.NhanVT.ModelExtensions.SearchAppointmentsTienDm;
using ApproveAppointmentDto = DNATestingSystem.Repository.TienDM.ModelExtensions.ApproveAppointmentDto;
using DenyAppointmentDto = DNATestingSystem.Repository.TienDM.ModelExtensions.DenyAppointmentDto;
using UpdateAppointmentStatusDto = DNATestingSystem.Repository.TienDM.ModelExtensions.UpdateAppointmentStatusDto;
using AppointmentTimelineDto = DNATestingSystem.Repository.TienDM.ModelExtensions.AppointmentTimelineDto;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DNATestingSystem.APIServices.BE.TienDM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "1,2")]
    public class AppointmentsTienDMController : ControllerBase
    {
        private readonly IAppointmentsTienDmService _appointmentsTienDmService;

        public AppointmentsTienDMController(IAppointmentsTienDmService appointmentsTienDmService)
        {
            _appointmentsTienDmService = appointmentsTienDmService;
        }

        // GET api/AppointmentsTienDM - Get all appointments
        [HttpGet]
        public async Task<ActionResult<List<AppointmentsTienDm>>> GetAll()
        {
            var appointments = await _appointmentsTienDmService.GetAllAsync();
            return Ok(appointments);
        }        // GET api/AppointmentsTienDM/paginated - Get all appointments with pagination
        [HttpGet("paginated")]
        public async Task<ActionResult<PaginationResult<List<AppointmentsTienDm>>>> GetAllPaginated(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            // Create empty search request to get all items
            var searchRequest = new SearchAppointmentsTienDm
            {
                CurrentPage = page,
                PageSize = pageSize
            };

            var result = await _appointmentsTienDmService.SearchAsync(searchRequest);
            return Ok(result);
        }

        // POST api/AppointmentsTienDM/paginated - Get all appointments with pagination (POST version)
        [HttpPost("paginated")]
        public async Task<ActionResult<PaginationResult<List<AppointmentsTienDm>>>> GetAllPaginatedPost([FromBody] PaginationRequest paginationRequest)
        {
            if (paginationRequest == null)
            {
                return BadRequest("Pagination request cannot be null");
            }

            // Create empty search request to get all items
            var searchRequest = new SearchAppointmentsTienDm
            {
                CurrentPage = paginationRequest.Page ?? 1,
                PageSize = paginationRequest.PageSize ?? 10
            };

            var result = await _appointmentsTienDmService.SearchAsync(searchRequest);
            return Ok(result);
        }

        // GET api/AppointmentsTienDM/{id} - Get appointment by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentsTienDm>> GetById(int id)
        {
            var appointment = await _appointmentsTienDmService.GetByIdAsync(id);
            if (appointment?.AppointmentsTienDmid == 0)
                return NotFound();
            return Ok(appointment);
        }

        // POST api/AppointmentsTienDM - Create new appointment using DTO
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateAppointmentsTienDmDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Auto-assign UserAccountId from JWT token if available
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                createDto.UserAccountId = userId;
            }

            var result = await _appointmentsTienDmService.CreateFromDtoAsync(createDto);
            if (result > 0)
                return CreatedAtAction(nameof(GetById), new { id = result }, result);
            return BadRequest("Failed to create appointment");
        }

        // PUT api/AppointmentsTienDM/{id} - Update existing appointment using DTO
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Update(int id, [FromBody] UpdateAppointmentsTienDmDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Set the ID from route parameter
            updateDto.AppointmentsTienDmid = id;

            var result = await _appointmentsTienDmService.UpdateFromDtoAsync(updateDto);
            if (result > 0)
                return Ok(result);
            return NotFound("Appointment not found or failed to update");
        }// DELETE api/AppointmentsTienDM/{id} - Delete appointment

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _appointmentsTienDmService.DeleteAsync(id);
            if (result)
                return Ok(true);
            return NotFound();
        }

        // POST api/AppointmentsTienDM/search - Search appointments using SearchRequest model
        [HttpPost("search")]
        public async Task<ActionResult<PaginationResult<List<AppointmentsTienDm>>>> Search([FromBody] SearchAppointmentsTienDm searchRequest)
        {
            if (searchRequest == null)
            {
                return BadRequest("Search request cannot be null");
            }

            // Set default values for pagination if not provided
            searchRequest.CurrentPage ??= 1;
            searchRequest.PageSize ??= 10;

            var result = await _appointmentsTienDmService.SearchAsync(searchRequest);
            return Ok(result);
        }        // POST api/AppointmentsTienDM/search/all - Search appointments and return all results (non-paginated)
        [HttpPost("search/all")]
        public async Task<ActionResult<List<AppointmentsTienDm>>> SearchAll([FromBody] SearchAppointmentsTienDm searchRequest)
        {
            if (searchRequest == null)
            {
                return BadRequest("Search request cannot be null");
            }

            // Set large page size to get all results
            searchRequest.CurrentPage = 1;
            searchRequest.PageSize = 10000;

            var result = await _appointmentsTienDmService.SearchAsync(searchRequest);
            return Ok(result?.Items ?? new List<AppointmentsTienDm>());
        }

        // POST api/AppointmentsTienDM/search/simple - Simple search with basic parameters (creates SearchRequest internally)
        //[HttpPost("search/simple")]
        //public async Task<ActionResult<PaginationResult<List<AppointmentsTienDm>>>> SearchSimple([FromBody] SimpleSearchRequest request)
        //{
        //    if (request == null)
        //    {
        //        return BadRequest("Search request cannot be null");
        //    }

        //    var searchRequest = new SearchAppointmentsTienDm
        //    {
        //        AppointmentsTienDmid = request.Id,
        //        ContactPhone = request.ContactPhone,
        //        TotalAmount = request.TotalAmount,
        //        CurrentPage = request.Page ?? 1,
        //        PageSize = request.PageSize ?? 10
        //    };

        //    var result = await _appointmentsTienDmService.SearchAsync(searchRequest);
        //    return Ok(result);
        //}

        // DTO-based endpoints
        // POST api/AppointmentsTienDM/dto - Create appointment using DTO
        //[HttpPost("dto")]
        //public async Task<ActionResult<int>> CreateFromDto([FromBody] CreateAppointmentsTienDmDto createDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        var id = await _appointmentsTienDmService.CreateFromDtoAsync(createDto);
        //        return Ok(new { id = id, message = "Appointment created successfully" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "Error creating appointment", error = ex.Message });
        //    }
        //}

        // PUT api/AppointmentsTienDM/dto/{id} - Update appointment using DTO
        //[HttpPut("dto/{id}")]
        //public async Task<ActionResult> UpdateFromDto(int id, [FromBody] UpdateAppointmentsTienDmDto updateDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != updateDto.AppointmentsTienDmid)
        //    {
        //        return BadRequest("ID mismatch");
        //    }

        //    try
        //    {
        //        var result = await _appointmentsTienDmService.UpdateFromDtoAsync(updateDto);
        //        if (result > 0)
        //        {
        //            return Ok(new { message = "Appointment updated successfully" });
        //        }
        //        else
        //        {
        //            return NotFound(new { message = "Appointment not found" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "Error updating appointment", error = ex.Message });
        //    }
        //}

        // GET api/AppointmentsTienDM/dto/{id} - Get appointment with display data
        //[HttpGet("dto/{id}")]
        //public async Task<ActionResult<AppointmentsTienDmDisplayDto>> GetDisplayDto(int id)
        //{
        //    try
        //    {
        //        var appointment = await _appointmentsTienDmService.GetDisplayDtoByIdAsync(id);
        //        if (appointment == null)
        //        {
        //            return NotFound(new { message = "Appointment not found" });
        //        }
        //        return Ok(appointment);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "Error retrieving appointment", error = ex.Message });
        //    }
        //}

        // POST api/AppointmentsTienDM/dto/search - Search appointments returning display DTOs
        //[HttpPost("dto/search")]
        //public async Task<ActionResult<PaginationResult<List<AppointmentsTienDmDisplayDto>>>> SearchDisplayDtos([FromBody] SearchAppointmentsTienDm searchRequest)
        //{
        //    try
        //    {
        //        if (searchRequest == null)
        //        {
        //            searchRequest = new SearchAppointmentsTienDm { CurrentPage = 1, PageSize = 10 };
        //        }

        //        var result = await _appointmentsTienDmService.GetDisplayDtosPaginatedAsync(searchRequest);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "Error searching appointments", error = ex.Message });
        //    }
        //}

        // GET api/AppointmentsTienDM/{id}/display - Get appointment by ID as display DTO
        //[HttpGet("{id}/display")]
        //public async Task<ActionResult<AppointmentsTienDmDisplayDto>> GetDisplayById(int id)
        //{
        //    var appointment = await _appointmentsTienDmService.GetDisplayDtoByIdAsync(id);
        //    if (appointment == null)
        //        return NotFound("Appointment not found");
        //    return Ok(appointment);
        //}

        // PUT api/AppointmentsTienDM/{id}/approve - Approve appointment (Staff only)
        [HttpPut("{id}/approve")]
        //[Authorize(Roles = "2,3,4")] // Staff, Manager, Admin
        public async Task<ActionResult<bool>> ApproveAppointment(int id, [FromBody] ApproveAppointmentDto approveDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Ensure ID matches
            approveDto.AppointmentId = id;

            // Get user ID from JWT token for audit trail
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                approveDto.ApprovedBy = userId;
            }

            var result = await _appointmentsTienDmService.ApproveAppointmentAsync(approveDto);
            if (result)
                return Ok(true);
            return NotFound("Appointment not found or cannot be approved");
        }

        // PUT api/AppointmentsTienDM/{id}/deny - Deny appointment (Staff only)
        [HttpPut("{id}/deny")]
        //[Authorize(Roles = "2,3,4")] // Staff, Manager, Admin
        public async Task<ActionResult<bool>> DenyAppointment(int id, [FromBody] DenyAppointmentDto denyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Ensure ID matches
            denyDto.AppointmentId = id;

            // Get user ID from JWT token for audit trail
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                denyDto.DeniedBy = userId;
            }

            var result = await _appointmentsTienDmService.DenyAppointmentAsync(denyDto);
            if (result)
                return Ok(true);
            return NotFound("Appointment not found or cannot be denied");
        }

        // PUT api/AppointmentsTienDM/{id}/update-status - Update appointment status
        [HttpPut("{id}/update-status")]
        //[Authorize(Roles = "2,3,4")] // Staff, Manager, Admin
        public async Task<ActionResult<bool>> UpdateAppointmentStatus(int id, [FromBody] UpdateAppointmentStatusDto statusDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Ensure ID matches
            statusDto.AppointmentId = id;

            // Get user ID from JWT token for audit trail
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                statusDto.UpdatedBy = userId;
            }

            var result = await _appointmentsTienDmService.UpdateAppointmentStatusAsync(statusDto);
            if (result)
                return Ok(true);
            return NotFound("Appointment not found or status cannot be updated");
        }

        // GET api/AppointmentsTienDM/{id}/timeline - Get appointment timeline for customer
        [HttpGet("{id}/timeline")]
        public async Task<ActionResult<AppointmentTimelineDto>> GetAppointmentTimeline(int id)
        {
            var timeline = await _appointmentsTienDmService.GetAppointmentTimelineAsync(id);
            if (timeline == null)
                return NotFound("Appointment not found");
            return Ok(timeline);
        }

        // GET api/AppointmentsTienDM/pending - Get pending appointments for staff
        [HttpGet("pending")]
        //[Authorize(Roles = "2,3,4")] // Staff, Manager, Admin
        public async Task<ActionResult<List<AppointmentsTienDmDisplayDto>>> GetPendingAppointments()
        {
            var pendingAppointments = await _appointmentsTienDmService.GetPendingAppointmentsForStaffAsync();
            return Ok(pendingAppointments);
        }

        // GET api/AppointmentsTienDM/user/{userId} - Get appointments for a specific user
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<AppointmentsTienDmDisplayDto>>> GetAppointmentsByUser(int userId)
        {
            var searchRequest = new SearchAppointmentsTienDm
            {
                CurrentPage = 1,
                PageSize = 100 // Get all appointments for the user
            };

            var result = await _appointmentsTienDmService.SearchAsync(searchRequest);

            // Filter by user ID (this should be handled in the service layer for better performance)
            var userAppointments = result.Items?.Where(a => a.UserAccountId == userId).ToList() ?? new List<AppointmentsTienDm>();

            // Convert to display DTOs
            var displayDtos = new List<AppointmentsTienDmDisplayDto>();
            foreach (var appointment in userAppointments)
            {
                var displayDto = await _appointmentsTienDmService.GetDisplayDtoByIdAsync(appointment.AppointmentsTienDmid);
                if (displayDto != null)
                {
                    displayDtos.Add(displayDto);
                }
            }

            return Ok(displayDtos);
        }

        // GET api/AppointmentsTienDM/status/{statusId} - Get appointments by status
        [HttpGet("status/{statusId}")]
        public async Task<ActionResult<PaginationResult<List<AppointmentsTienDm>>>> GetAppointmentsByStatus(
            int statusId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var searchRequest = new SearchAppointmentsTienDm
            {
                AppointmentStatusesTienDmid = statusId,
                CurrentPage = page,
                PageSize = pageSize
            };

            var result = await _appointmentsTienDmService.SearchAsync(searchRequest);
            return Ok(result);
        }

        // GET api/AppointmentsTienDM/today - Get today's appointments
        [HttpGet("today")]
        public async Task<ActionResult<List<AppointmentsTienDmDisplayDto>>> GetTodaysAppointments()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            // Get all appointments for today (this should be enhanced with proper date filtering in service layer)
            var searchRequest = new SearchAppointmentsTienDm
            {
                CurrentPage = 1,
                PageSize = 100
            };

            var result = await _appointmentsTienDmService.SearchAsync(searchRequest);

            // Filter by today's date (this should be handled in the service layer for better performance)
            var todaysAppointments = result.Items?.Where(a => a.AppointmentDate == today).ToList() ?? new List<AppointmentsTienDm>();

            // Convert to display DTOs
            var displayDtos = new List<AppointmentsTienDmDisplayDto>();
            foreach (var appointment in todaysAppointments)
            {
                var displayDto = await _appointmentsTienDmService.GetDisplayDtoByIdAsync(appointment.AppointmentsTienDmid);
                if (displayDto != null)
                {
                    displayDtos.Add(displayDto);
                }
            }

            return Ok(displayDtos);
        }

        // GET api/AppointmentsTienDM/{id}/display - Get detailed appointment display DTO
        [HttpGet("{id}/display")]
        public async Task<ActionResult<AppointmentsTienDmDisplayDto>> GetAppointmentDisplay(int id)
        {
            var displayDto = await _appointmentsTienDmService.GetDisplayDtoByIdAsync(id);
            if (displayDto == null)
                return NotFound("Appointment not found");
            return Ok(displayDto);
        }
    }
}
