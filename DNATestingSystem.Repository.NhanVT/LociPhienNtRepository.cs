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
    public class LociPhienNtRepository : GenericRepository<LociPhienNt>
    {
        public LociPhienNtRepository() { }

        public LociPhienNtRepository(SE18_PRN232_SE1730_G3_DNATestingSystemContext context) => _context = context;

        public async Task<List<LociPhienNt>> GetAllLociAsync()
        {
            var allLoci = await _context.LociPhienNts.ToListAsync();
            return allLoci ?? new List<LociPhienNt>();
        }

        public async Task<LociPhienNt> GetLocusByIdAsync(int id)
        {
            var locus = await _context.LociPhienNts
                .FirstOrDefaultAsync(l => l.PhienNtid == id);
            return locus ?? new LociPhienNt();
        }

        public async Task<LociPhienNt> GetLocusByNameAsync(string name)
        {
            var locus = await _context.LociPhienNts
                .FirstOrDefaultAsync(l => l.Name == name);
            return locus ?? new LociPhienNt();
        }

        public async Task<List<LociPhienNt>> GetCodisLociAsync()
        {
            var codisLoci = await _context.LociPhienNts
                .Where(l => l.IsCodis == true)
                .ToListAsync();
            return codisLoci ?? new List<LociPhienNt>();
        }

        public async Task<PaginationResult<List<LociPhienNt>>> SearchLociWithPagingAsync(string name, bool? isCodis, int page, int pageSize)
        {
            var lociQuery = _context.LociPhienNts.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                lociQuery = lociQuery.Where(l => l.Name.Contains(name));
            }

            if (isCodis.HasValue)
            {
                lociQuery = lociQuery.Where(l => l.IsCodis == isCodis.Value);
            }

            var loci = await lociQuery.ToListAsync();
            var totalItems = loci.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            loci = loci.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var result = new PaginationResult<List<LociPhienNt>>
            {
                TotalItems = totalItems,
                TotalPage = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = loci
            };
            return result;
        }
    }
}
