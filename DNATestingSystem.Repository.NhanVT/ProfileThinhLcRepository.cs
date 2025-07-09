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
    public class ProfileThinhLcRepository : GenericRepository<ProfileThinhLc>
    {
        public ProfileThinhLcRepository() { }

        public ProfileThinhLcRepository(SE18_PRN232_SE1730_G3_DNATestingSystemContext context) => _context = context;

        public async Task<List<ProfileThinhLc>> GetAllProfilesAsync()
        {
            var allProfiles = await _context.ProfileThinhLcs
                .Include(p => p.UserAccount)
                .ToListAsync();
            return allProfiles ?? new List<ProfileThinhLc>();
        }

        public async Task<ProfileThinhLc> GetProfileByIdAsync(int id)
        {
            var profile = await _context.ProfileThinhLcs
                .Include(p => p.UserAccount)
                .FirstOrDefaultAsync(p => p.ProfileThinhLcid == id);
            return profile ?? new ProfileThinhLc();
        }

        public async Task<ProfileThinhLc> GetProfileByNationalIdAsync(string nationalId)
        {
            var profile = await _context.ProfileThinhLcs
                .Include(p => p.UserAccount)
                .FirstOrDefaultAsync(p => p.NationalId == nationalId);
            return profile ?? new ProfileThinhLc();
        }
        
        public async Task<ProfileThinhLc> GetByNationalIdAsync(string nationalId)
        {
            return await GetProfileByNationalIdAsync(nationalId);
        }

        public async Task<List<ProfileThinhLc>> GetProfilesByUserIdAsync(int userId)
        {
            var profiles = await _context.ProfileThinhLcs
                .Include(p => p.UserAccount)
                .Where(p => p.UserAccountId == userId)
                .ToListAsync();
            return profiles ?? new List<ProfileThinhLc>();
        }
        
        public async Task<List<ProfileThinhLc>> GetByUserIdAsync(int userId)
        {
            return await GetProfilesByUserIdAsync(userId);
        }
        
        public async Task<List<ProfileThinhLc>> GetByGenderAsync(string gender)
        {
            var profiles = await _context.ProfileThinhLcs
                .Include(p => p.UserAccount)
                .Where(p => p.Gender == gender)
                .ToListAsync();
            return profiles ?? new List<ProfileThinhLc>();
        }

        public async Task<PaginationResult<List<ProfileThinhLc>>> SearchProfilesWithPagingAsync(string fullName, string nationalId, string gender, int page, int pageSize)
        {
            var profilesQuery = _context.ProfileThinhLcs
                .Include(p => p.UserAccount)
                .AsQueryable();

            if (!string.IsNullOrEmpty(fullName))
            {
                profilesQuery = profilesQuery.Where(p => p.FullName.Contains(fullName));
            }

            if (!string.IsNullOrEmpty(nationalId))
            {
                profilesQuery = profilesQuery.Where(p => p.NationalId.Contains(nationalId));
            }

            if (!string.IsNullOrEmpty(gender))
            {
                profilesQuery = profilesQuery.Where(p => p.Gender == gender);
            }

            var profiles = await profilesQuery.ToListAsync();
            var totalItems = profiles.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            profiles = profiles.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var result = new PaginationResult<List<ProfileThinhLc>>
            {
                TotalItems = totalItems,
                TotalPage = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = profiles
            };
            return result;
        }
    }
}
