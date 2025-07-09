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
    public class ProfileThinhLcService : IProfileThinhLcService
    {
        private readonly ProfileThinhLcRepository _repository;

        public ProfileThinhLcService()
            => _repository = new ProfileThinhLcRepository();

        public async Task<List<ProfileThinhLc>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ProfileThinhLc> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<ProfileThinhLc> GetByNationalIdAsync(string nationalId)
        {
            return await _repository.GetByNationalIdAsync(nationalId);
        }

        public async Task<List<ProfileThinhLc>> GetByUserIdAsync(int userId)
        {
            return await _repository.GetByUserIdAsync(userId);
        }
        
        public async Task<List<ProfileThinhLc>> GetByGenderAsync(string gender)
        {
            return await _repository.GetByGenderAsync(gender);
        }

        public async Task<int> CreateAsync(ProfileThinhLc profile)
        {
            return await _repository.CreateAsync(profile);
        }

        public async Task<int> UpdateAsync(ProfileThinhLc profile)
        {
            return await _repository.UpdateAsync(profile);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var profile = await _repository.GetByIdAsync(id);
            if (profile == null) return false;
            return await _repository.RemoveAsync(profile);
        }

        public async Task<PaginationResult<List<ProfileThinhLc>>> SearchProfilesWithPagingAsync(string fullName, string nationalId, string gender, int page, int pageSize)
        {
            return await _repository.SearchProfilesWithPagingAsync(fullName, nationalId, gender, page, pageSize);
        }

        // Legacy method implementations for compatibility
        public async Task<List<ProfileThinhLc>> GetAllProfilesAsync()
        {
            return await GetAllAsync();
        }

        public async Task<ProfileThinhLc> GetProfileByIdAsync(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<ProfileThinhLc> GetProfileByNationalIdAsync(string nationalId)
        {
            return await GetByNationalIdAsync(nationalId);
        }

        public async Task<List<ProfileThinhLc>> GetProfilesByUserIdAsync(int userId)
        {
            return await GetByUserIdAsync(userId);
        }

        public async Task<int> CreateProfileAsync(ProfileThinhLc profile)
        {
            return await CreateAsync(profile);
        }

        public async Task<int> UpdateProfileAsync(ProfileThinhLc profile)
        {
            return await UpdateAsync(profile);
        }

        public async Task<bool> DeleteProfileAsync(int id)
        {
            return await DeleteAsync(id);
        }
    }
}
