using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using DNATestingSystem.Repository.NhanVT.Models;

namespace DNATestingSystem.Services.NhanVT
{
    public interface IProfileThinhLcService
    {
        Task<List<ProfileThinhLc>> GetAllAsync();
        Task<ProfileThinhLc> GetByIdAsync(int id);
        Task<ProfileThinhLc> GetByNationalIdAsync(string nationalId);
        Task<List<ProfileThinhLc>> GetByUserIdAsync(int userId);
        Task<List<ProfileThinhLc>> GetByGenderAsync(string gender);
        Task<int> CreateAsync(ProfileThinhLc profile);
        Task<int> UpdateAsync(ProfileThinhLc profile);
        Task<bool> DeleteAsync(int id);
        Task<PaginationResult<List<ProfileThinhLc>>> SearchProfilesWithPagingAsync(string fullName, string nationalId, string gender, int page, int pageSize);

        // Legacy method names for compatibility
        Task<List<ProfileThinhLc>> GetAllProfilesAsync();
        Task<ProfileThinhLc> GetProfileByIdAsync(int id);
        Task<ProfileThinhLc> GetProfileByNationalIdAsync(string nationalId);
        Task<List<ProfileThinhLc>> GetProfilesByUserIdAsync(int userId);
        Task<int> CreateProfileAsync(ProfileThinhLc profile);
        Task<int> UpdateProfileAsync(ProfileThinhLc profile);
        Task<bool> DeleteProfileAsync(int id);
    }
}
