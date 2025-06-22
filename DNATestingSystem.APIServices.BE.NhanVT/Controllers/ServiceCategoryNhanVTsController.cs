using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using DNATestingSystem.Repository.NhanVT.Models;
using DNATestingSystem.Services.NhanVT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DNATestingSystem.APIServices.BE.NhanVT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCategoryNhanVTsController : ControllerBase
    {
        private readonly IServicesCategoryNhanVTService _servicesCategoryNhanVTService;

        public ServiceCategoryNhanVTsController(IServicesCategoryNhanVTService servicesCategoryNhanVTService) => _servicesCategoryNhanVTService = servicesCategoryNhanVTService;
        // GET: api/<ServiceNhanVTsController>
        [HttpGet]
        [Authorize(Roles = "1,2")]

        public async Task<IEnumerable<ServiceCategoriesNhanVt>> Get()
        {
            return await _servicesCategoryNhanVTService.GetAllServicesCategoryAsync();
        }

        // GET api/<ServiceCategoryNhanVTsController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "1,2")]

        public async Task<ServiceCategoriesNhanVt> Get(int id)
        {
            return await _servicesCategoryNhanVTService.GetServiceCategoryByIdAsync(id);
        }

        [HttpPost]
        [Authorize(Roles = "1,2")]

        public async Task<int> Post(ServiceCategoriesNhanVt serviceCategory)
        {
            return await _servicesCategoryNhanVTService.CreateServiceCategoryAsync(serviceCategory);
        }

        // PUT api/<ServiceNhanVTsController>/5
        [HttpPut("{id}")]
        public async Task<int> Put(ServiceCategoriesNhanVt servicesNhanVt)
        {
            return await _servicesCategoryNhanVTService.UpdateServiceCategoryAsync(servicesNhanVt);

        }

        // DELETE api/<ServiceNhanVTsController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _servicesCategoryNhanVTService.DeleteServiceCategoryAsync(id);
        }

    }
}
