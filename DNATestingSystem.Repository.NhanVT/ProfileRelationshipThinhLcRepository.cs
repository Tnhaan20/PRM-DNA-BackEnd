using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNATestingSystem.Repository.NhanVT.Basic;
using DNATestingSystem.Repository.NhanVT.DBContext;
using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using DNATestingSystem.Repository.NhanVT.Models;
using Microsoft.EntityFrameworkCore;

namespace DNATestingSystem.Repository.NhanVT
{
    public class ProfileRelationshipThinhLcRepository : GenericRepository<ProfileRelationshipThinhLc>
    {
        public ProfileRelationshipThinhLcRepository() { }

        public ProfileRelationshipThinhLcRepository(SE18_PRN232_SE1730_G3_DNATestingSystemContext context) => _context = context;

        public async Task<List<ProfileRelationshipThinhLc>> GetAllProfileRelationshipsAsync()
        {
            var allRelationships = await _context.ProfileRelationshipThinhLcs
                .Include(pr => pr.ProfileThinhLcid1Navigation)
                .Include(pr => pr.ProfileThinhLcid2Navigation)
                .ToListAsync();
            return allRelationships ?? new List<ProfileRelationshipThinhLc>();
        }

        public async Task<ProfileRelationshipThinhLc> GetProfileRelationshipByIdAsync(int id)
        {
            var relationship = await _context.ProfileRelationshipThinhLcs
                .Include(pr => pr.ProfileThinhLcid1Navigation)
                .Include(pr => pr.ProfileThinhLcid2Navigation)
                .FirstOrDefaultAsync(pr => pr.ProfileRelationshipThinhLcid == id);
            return relationship ?? new ProfileRelationshipThinhLc();
        }

        public async Task<List<ProfileRelationshipThinhLc>> GetRelationshipsByProfileIdAsync(int profileId)
        {
            var relationships = await _context.ProfileRelationshipThinhLcs
                .Include(pr => pr.ProfileThinhLcid1Navigation)
                .Include(pr => pr.ProfileThinhLcid2Navigation)
                .Where(pr => pr.ProfileThinhLcid1 == profileId || pr.ProfileThinhLcid2 == profileId)
                .ToListAsync();
            return relationships ?? new List<ProfileRelationshipThinhLc>();
        }
        
        public async Task<List<ProfileRelationshipThinhLc>> GetByProfileIdAsync(int profileId)
        {
            return await GetRelationshipsByProfileIdAsync(profileId);
        }

        public async Task<List<ProfileRelationshipThinhLc>> GetRelationshipsByTypeAsync(string relationshipType)
        {
            var relationships = await _context.ProfileRelationshipThinhLcs
                .Include(pr => pr.ProfileThinhLcid1Navigation)
                .Include(pr => pr.ProfileThinhLcid2Navigation)
                .Where(pr => pr.RelationshipType != null && pr.RelationshipType.Contains(relationshipType))
                .ToListAsync();
            return relationships ?? new List<ProfileRelationshipThinhLc>();
        }
        
        public async Task<List<ProfileRelationshipThinhLc>> GetByRelationshipTypeAsync(string relationshipType)
        {
            return await GetRelationshipsByTypeAsync(relationshipType);
        }

        public async Task<PaginationResult<List<ProfileRelationshipThinhLc>>> SearchProfileRelationshipsWithPagingAsync(string relationshipType, int? profileId, int page, int pageSize)
        {
            var relationshipsQuery = _context.ProfileRelationshipThinhLcs
                .Include(pr => pr.ProfileThinhLcid1Navigation)
                .Include(pr => pr.ProfileThinhLcid2Navigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(relationshipType))
            {
                relationshipsQuery = relationshipsQuery.Where(pr => pr.RelationshipType != null && pr.RelationshipType.Contains(relationshipType));
            }

            if (profileId.HasValue)
            {
                relationshipsQuery = relationshipsQuery.Where(pr => pr.ProfileThinhLcid1 == profileId.Value || pr.ProfileThinhLcid2 == profileId.Value);
            }

            var relationships = await relationshipsQuery.ToListAsync();
            var totalItems = relationships.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            relationships = relationships.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var result = new PaginationResult<List<ProfileRelationshipThinhLc>>
            {
                TotalItems = totalItems,
                TotalPage = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = relationships
            };
            return result;
        }

        public async Task<PaginationResult<List<ProfileRelationshipThinhLc>>> SearchWithPagingAsync(string relationshipType, int? profileId, int page, int pageSize)
        {
            return await SearchProfileRelationshipsWithPagingAsync(relationshipType, profileId, page, pageSize);
        }
    }
}
