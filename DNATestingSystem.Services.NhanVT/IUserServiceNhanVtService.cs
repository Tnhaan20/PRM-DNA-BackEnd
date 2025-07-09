using DNATestingSystem.Repository.NhanVT.Models;
using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATestingSystem.Services.NhanVT
{
    public interface IUserServiceNhanVtService
    {
        Task<List<UserServiceNhanVt>> GetAllAsync();
        Task<UserServiceNhanVt> GetByIdAsync(int id);
        Task<List<UserServiceNhanVt>> GetByUserIdAsync(int userId);
        Task<List<UserServiceNhanVt>> GetByServiceIdAsync(int serviceId);
        Task<List<UserServiceNhanVt>> GetActiveAssignmentsAsync();
        Task<List<UserServiceNhanVt>> GetByRoleAsync(string role);
        Task<List<UserServiceNhanVt>> GetActiveServicesAsync();
        Task<PaginationResult<List<UserServiceNhanVt>>> SearchAsync(int? userId, int? serviceId, string role, bool? isActive, int page, int pageSize);
        Task<int> CreateAsync(UserServiceNhanVt entity);
        Task<int> UpdateAsync(UserServiceNhanVt entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> ActivateAsync(int id);
        Task<bool> DeactivateAsync(int id);
    }
}
