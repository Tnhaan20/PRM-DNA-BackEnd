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
    public class ProfileRelationshipThinhLcService : IProfileRelationshipThinhLcService
    {
        private readonly ProfileRelationshipThinhLcRepository _repository;

        public ProfileRelationshipThinhLcService()
            => _repository = new ProfileRelationshipThinhLcRepository();

        public async Task<List<ProfileRelationshipThinhLc>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ProfileRelationshipThinhLc> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<ProfileRelationshipThinhLc>> GetByProfileIdAsync(int profileId)
        {
            return await _repository.GetByProfileIdAsync(profileId);
        }

        public async Task<List<ProfileRelationshipThinhLc>> GetByRelationshipTypeAsync(string relationshipType)
        {
            return await _repository.GetByRelationshipTypeAsync(relationshipType);
        }

        public async Task<int> CreateAsync(ProfileRelationshipThinhLc entity)
        {
            return await _repository.CreateAsync(entity);
        }

        public async Task<int> UpdateAsync(ProfileRelationshipThinhLc entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;
            return await _repository.RemoveAsync(entity);
        }

        public async Task<PaginationResult<List<ProfileRelationshipThinhLc>>> SearchWithPagingAsync(string relationshipType, int? profileId, int page, int pageSize)
        {
            return await _repository.SearchWithPagingAsync(relationshipType, profileId, page, pageSize);
        }

        // Legacy method implementations for compatibility
        public async Task<List<ProfileRelationshipThinhLc>> GetAllProfileRelationshipsAsync()
        {
            return await GetAllAsync();
        }

        public async Task<ProfileRelationshipThinhLc> GetProfileRelationshipByIdAsync(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<List<ProfileRelationshipThinhLc>> GetRelationshipsByProfileIdAsync(int profileId)
        {
            return await GetByProfileIdAsync(profileId);
        }

        public async Task<List<ProfileRelationshipThinhLc>> GetRelationshipsByTypeAsync(string relationshipType)
        {
            return await GetByRelationshipTypeAsync(relationshipType);
        }

        public async Task<int> CreateProfileRelationshipAsync(ProfileRelationshipThinhLc profileRelationship)
        {
            return await CreateAsync(profileRelationship);
        }

        public async Task<int> UpdateProfileRelationshipAsync(ProfileRelationshipThinhLc profileRelationship)
        {
            return await UpdateAsync(profileRelationship);
        }

        public async Task<bool> DeleteProfileRelationshipAsync(int id)
        {
            return await DeleteAsync(id);
        }

        public async Task<PaginationResult<List<ProfileRelationshipThinhLc>>> SearchProfileRelationshipsWithPagingAsync(string relationshipType, int? profileId, int page, int pageSize)
        {
            return await SearchWithPagingAsync(relationshipType, profileId, page, pageSize);
        }
    }
}
