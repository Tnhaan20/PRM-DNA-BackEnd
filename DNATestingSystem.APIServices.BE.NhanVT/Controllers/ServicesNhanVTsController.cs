using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using DNATestingSystem.Repository.NhanVT.Models;
using DNATestingSystem.Services.NhanVT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DNATestingSystem.APIServices.BE.NhanVT.Helpers;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DNATestingSystem.APIServices.BE.NhanVT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesNhanVTsController : ControllerBase
    {
        private readonly IServicesNhanVTService _servicesNhanVTService;
        private readonly ILogger<ServicesNhanVTsController> _logger;

        public ServicesNhanVTsController(
            IServicesNhanVTService servicesNhanVTService, 
            ILogger<ServicesNhanVTsController> logger)
        {
            _servicesNhanVTService = servicesNhanVTService;
            _logger = logger;
        }
        // GET: api/<ServiceNhanVTsController>
        [HttpGet]
        [Authorize(Roles = "1,2")]
        public async Task<IEnumerable<ServicesNhanVt>> Get()
        {
            return await _servicesNhanVTService.GetAllServicesAsync();
        }

        // GET api/<ServiceNhanVTsController>/5
        [HttpGet("{id}")]
        public async Task<ServicesNhanVt> Get(int id)
        {
            var result = await _servicesNhanVTService.GetServicesByIdAsync(id);
            
            // Log the serialized result to verify navigation properties are excluded
            _logger.LogInformation($"Serialized service: {result.ToJson()}");
            
            return result;
        }

        // POST api/<ServiceNhanVTsController>
        [HttpPost]
        public async Task<int> Post(ServicesNhanVt servicesNhanVt)
        {
            return await _servicesNhanVTService.CreateServiceAsync(servicesNhanVt);
        }

        // PUT api/<ServiceNhanVTsController>/5
        [HttpPut("{id}")]
        public async Task<int> Put(ServicesNhanVt servicesNhanVt)
        {
            return await _servicesNhanVTService.UpdateServiceAsync(servicesNhanVt);

        }

        // DELETE api/<ServiceNhanVTsController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _servicesNhanVTService.DeleteServiceAsync(id);
        }

        [HttpGet("{serviceName}/{servicePrice}/{categoryName}")]
        public async Task<IEnumerable<ServicesNhanVt>> Get(string serviceName, decimal? servicePrice, string categoryName)
        {
            return await _servicesNhanVTService.SearchServiceAsync(serviceName, servicePrice, categoryName);
        }

        [HttpGet("{serviceName}/{servicePrice}/{categoryName}/{page}/{pageSize}")]
        [Authorize(Roles ="1,2")]
        public async Task<PaginationResult<List<ServicesNhanVt>>> SearchWithPagingAsync(string serviceName, decimal? servicePrice, string categoryName, int page, int pageSize)
        {
           return await _servicesNhanVTService.SearchWithPagingAsync(serviceName, servicePrice,categoryName,page,pageSize);
        }

        [HttpPost("Search")]
        [Authorize(Roles = "1,2")]
        public async Task<PaginationResult<List<ServicesNhanVt>>> SearchServicesWithPagingAsync(SearchServices search)
        {
            return await _servicesNhanVTService.SearchServiceWithPagingAsync(search);
        }

        [HttpGet("{page}/{pageSize}")]
        [Authorize(Roles = "1,2")]
        public async Task<PaginationResult<List<ServicesNhanVt>>> SearchAllWithPagingAsync(int page, int pageSize)
        {
            return await _servicesNhanVTService.SearchAllWithPagingAsync(page, pageSize);
        }
    }
}
