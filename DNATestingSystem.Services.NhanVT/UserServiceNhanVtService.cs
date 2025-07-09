using DNATestingSystem.Repository.NhanVT.Models;
using DNATestingSystem.Repository.NhanVT;
using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATestingSystem.Services.NhanVT
{
    public class UserServiceNhanVtService : IUserServiceNhanVtService
    {
        private readonly UserServiceNhanVtRepository _repository;

        public UserServiceNhanVtService()
            => _repository = new UserServiceNhanVtRepository();

        public async Task<List<UserServiceNhanVt>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<UserServiceNhanVt> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<UserServiceNhanVt>> GetByUserIdAsync(int userId)
        {
            return await _repository.GetByUserIdAsync(userId);
        }

        public async Task<List<UserServiceNhanVt>> GetByServiceIdAsync(int serviceId)
        {
            return await _repository.GetByServiceIdAsync(serviceId);
        }

        public async Task<List<UserServiceNhanVt>> GetActiveAssignmentsAsync()
        {
            return await _repository.GetActiveAssignmentsAsync();
        }
        
        public async Task<List<UserServiceNhanVt>> GetByRoleAsync(string role)
        {
            return await _repository.GetByRoleAsync(role);
        }

        public async Task<List<UserServiceNhanVt>> GetActiveServicesAsync()
        {
            return await _repository.GetActiveServicesAsync();
        }

        public async Task<PaginationResult<List<UserServiceNhanVt>>> SearchAsync(int? userId, int? serviceId, string role, bool? isActive, int page, int pageSize)
        {
            return await _repository.SearchAsync(userId, serviceId, role, isActive, page, pageSize);
        }

        public async Task<int> CreateAsync(UserServiceNhanVt entity)
        {
            return await _repository.CreateAsync(entity);
        }

        public async Task<int> UpdateAsync(UserServiceNhanVt entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;
            return await _repository.RemoveAsync(entity);
        }

        public async Task<bool> ActivateAsync(int id)
        {
            return await _repository.ActivateAsync(id);
        }

        public async Task<bool> DeactivateAsync(int id)
        {
            return await _repository.DeactivateAsync(id);
        }
    }
}
