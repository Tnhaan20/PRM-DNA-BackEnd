using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using DNATestingSystem.Repository.NhanVT.Models;

namespace DNATestingSystem.Services.NhanVT
{
    public interface IProfileRelationshipThinhLcService
    {
        Task<List<ProfileRelationshipThinhLc>> GetAllAsync();
        Task<ProfileRelationshipThinhLc> GetByIdAsync(int id);
        Task<List<ProfileRelationshipThinhLc>> GetByProfileIdAsync(int profileId);
        Task<List<ProfileRelationshipThinhLc>> GetByRelationshipTypeAsync(string relationshipType);
        Task<int> CreateAsync(ProfileRelationshipThinhLc entity);
        Task<int> UpdateAsync(ProfileRelationshipThinhLc entity);
        Task<bool> DeleteAsync(int id);
        Task<PaginationResult<List<ProfileRelationshipThinhLc>>> SearchWithPagingAsync(string relationshipType, int? profileId, int page, int pageSize);
        
        // Legacy method names for compatibility
        Task<List<ProfileRelationshipThinhLc>> GetAllProfileRelationshipsAsync();
        Task<ProfileRelationshipThinhLc> GetProfileRelationshipByIdAsync(int id);
        Task<List<ProfileRelationshipThinhLc>> GetRelationshipsByProfileIdAsync(int profileId);
        Task<List<ProfileRelationshipThinhLc>> GetRelationshipsByTypeAsync(string relationshipType);
        Task<int> CreateProfileRelationshipAsync(ProfileRelationshipThinhLc profileRelationship);
        Task<int> UpdateProfileRelationshipAsync(ProfileRelationshipThinhLc profileRelationship);
        Task<bool> DeleteProfileRelationshipAsync(int id);
        Task<PaginationResult<List<ProfileRelationshipThinhLc>>> SearchProfileRelationshipsWithPagingAsync(string relationshipType, int? profileId, int page, int pageSize);
    }
}
