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
    public class SampleTypeThinhLcRepository : GenericRepository<SampleTypeThinhLc>
    {
        public SampleTypeThinhLcRepository() { }

        public SampleTypeThinhLcRepository(SE18_PRN232_SE1730_G3_DNATestingSystemContext context) => _context = context;

        public async Task<List<SampleTypeThinhLc>> GetAllSampleTypesAsync()
        {
            var allSampleTypes = await _context.SampleTypeThinhLcs.ToListAsync();
            return allSampleTypes ?? new List<SampleTypeThinhLc>();
        }

        public async Task<SampleTypeThinhLc> GetSampleTypeByIdAsync(int id)
        {
            var sampleType = await _context.SampleTypeThinhLcs
                .FirstOrDefaultAsync(st => st.SampleTypeThinhLcid == id);
            return sampleType ?? new SampleTypeThinhLc();
        }

        public async Task<SampleTypeThinhLc> GetSampleTypeByNameAsync(string typeName)
        {
            var sampleType = await _context.SampleTypeThinhLcs
                .FirstOrDefaultAsync(st => st.TypeName == typeName);
            return sampleType ?? new SampleTypeThinhLc();
        }

        public async Task<List<SampleTypeThinhLc>> GetActiveSampleTypesAsync()
        {
            var activeSampleTypes = await _context.SampleTypeThinhLcs
                .Where(st => st.IsActive == true)
                .ToListAsync();
            return activeSampleTypes ?? new List<SampleTypeThinhLc>();
        }

        public async Task<PaginationResult<List<SampleTypeThinhLc>>> SearchSampleTypesWithPagingAsync(string typeName, bool? isActive, int page, int pageSize)
        {
            var sampleTypesQuery = _context.SampleTypeThinhLcs.AsQueryable();

            if (!string.IsNullOrEmpty(typeName))
            {
                sampleTypesQuery = sampleTypesQuery.Where(st => st.TypeName.Contains(typeName));
            }

            if (isActive.HasValue)
            {
                sampleTypesQuery = sampleTypesQuery.Where(st => st.IsActive == isActive.Value);
            }

            var sampleTypes = await sampleTypesQuery.ToListAsync();
            var totalItems = sampleTypes.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            sampleTypes = sampleTypes.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var result = new PaginationResult<List<SampleTypeThinhLc>>
            {
                TotalItems = totalItems,
                TotalPage = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = sampleTypes
            };
            return result;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var sampleType = await _context.SampleTypeThinhLcs.FindAsync(id);
            if (sampleType == null) return false;

            sampleType.IsActive = false;
            sampleType.DeletedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<SampleTypeThinhLc> GetByTypeNameAsync(string typeName)
        {
            var result = await _context.SampleTypeThinhLcs
                .FirstOrDefaultAsync(st => st.TypeName == typeName);
            return result ?? new SampleTypeThinhLc();
        }

        public async Task<List<SampleTypeThinhLc>> GetActiveAsync()
        {
            return await _context.SampleTypeThinhLcs
                .Where(st => st.IsActive == true)
                .ToListAsync();
        }

        public async Task<bool> DeactivateAsync(int id)
        {
            var sampleType = await _context.SampleTypeThinhLcs.FindAsync(id);
            if (sampleType == null) return false;

            sampleType.IsActive = false;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActivateAsync(int id)
        {
            var sampleType = await _context.SampleTypeThinhLcs.FindAsync(id);
            if (sampleType == null) return false;

            sampleType.IsActive = true;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
