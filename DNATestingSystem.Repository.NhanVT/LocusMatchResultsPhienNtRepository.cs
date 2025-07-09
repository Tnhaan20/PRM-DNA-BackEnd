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
    public class LocusMatchResultsPhienNtRepository : GenericRepository<LocusMatchResultsPhienNt>
    {
        public LocusMatchResultsPhienNtRepository() { }

        public LocusMatchResultsPhienNtRepository(SE18_PRN232_SE1730_G3_DNATestingSystemContext context) => _context = context;

        public async Task<List<LocusMatchResultsPhienNt>> GetAllLocusMatchResultsAsync()
        {
            var allResults = await _context.LocusMatchResultsPhienNts
                .Include(l => l.Locus)
                .Include(l => l.Test)
                .ToListAsync();
            return allResults ?? new List<LocusMatchResultsPhienNt>();
        }

        public async Task<LocusMatchResultsPhienNt> GetLocusMatchResultByIdAsync(int id)
        {
            var result = await _context.LocusMatchResultsPhienNts
                .Include(l => l.Locus)
                .Include(l => l.Test)
                .FirstOrDefaultAsync(l => l.PhienNtid == id);
            return result ?? new LocusMatchResultsPhienNt();
        }

        public async Task<List<LocusMatchResultsPhienNt>> GetLocusMatchResultsByTestIdAsync(int testId)
        {
            var results = await _context.LocusMatchResultsPhienNts
                .Include(l => l.Locus)
                .Include(l => l.Test)
                .Where(l => l.TestId == testId)
                .ToListAsync();
            return results ?? new List<LocusMatchResultsPhienNt>();
        }

        public async Task<List<LocusMatchResultsPhienNt>> GetLocusMatchResultsByLocusIdAsync(int locusId)
        {
            var results = await _context.LocusMatchResultsPhienNts
                .Include(l => l.Locus)
                .Include(l => l.Test)
                .Where(l => l.LocusId == locusId)
                .ToListAsync();
            return results ?? new List<LocusMatchResultsPhienNt>();
        }

        public async Task<PaginationResult<List<LocusMatchResultsPhienNt>>> SearchLocusMatchResultsWithPagingAsync(int? testId, int? locusId, int page, int pageSize)
        {
            var resultsQuery = _context.LocusMatchResultsPhienNts
                .Include(l => l.Locus)
                .Include(l => l.Test)
                .AsQueryable();

            if (testId.HasValue)
            {
                resultsQuery = resultsQuery.Where(l => l.TestId == testId.Value);
            }

            if (locusId.HasValue)
            {
                resultsQuery = resultsQuery.Where(l => l.LocusId == locusId.Value);
            }

            var results = await resultsQuery.ToListAsync();
            var totalItems = results.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            results = results.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var result = new PaginationResult<List<LocusMatchResultsPhienNt>>
            {
                TotalItems = totalItems,
                TotalPage = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = results
            };
            return result;
        }
    }
}
