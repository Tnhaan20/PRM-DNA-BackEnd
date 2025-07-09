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
    public class AlleleResultsPhienNtRepository : GenericRepository<AlleleResultsPhienNt>
    {
        public AlleleResultsPhienNtRepository() { }

        public AlleleResultsPhienNtRepository(SE18_PRN232_SE1730_G3_DNATestingSystemContext context) => _context = context;

        public async Task<List<AlleleResultsPhienNt>> GetAllAlleleResultsAsync()
        {
            var allAlleleResults = await _context.AlleleResultsPhienNts
                .Include(a => a.Locus)
                .Include(a => a.ProfileThinhLc)
                .Include(a => a.Test)
                .ToListAsync();

            return allAlleleResults ?? new List<AlleleResultsPhienNt>();
        }

        public async Task<AlleleResultsPhienNt> GetAlleleResultByIdAsync(int id)
        {
            var alleleResult = await _context.AlleleResultsPhienNts
                .Include(a => a.Locus)
                .Include(a => a.ProfileThinhLc)
                .Include(a => a.Test)
                .FirstOrDefaultAsync(a => a.PhienNtid == id);

            return alleleResult ?? new AlleleResultsPhienNt();
        }

        public async Task<List<AlleleResultsPhienNt>> GetAlleleResultsByTestIdAsync(int testId)
        {
            var alleleResults = await _context.AlleleResultsPhienNts
                .Include(a => a.Locus)
                .Include(a => a.ProfileThinhLc)
                .Include(a => a.Test)
                .Where(a => a.TestId == testId)
                .ToListAsync();

            return alleleResults ?? new List<AlleleResultsPhienNt>();
        }

        public async Task<List<AlleleResultsPhienNt>> GetAlleleResultsByProfileIdAsync(int profileId)
        {
            var alleleResults = await _context.AlleleResultsPhienNts
                .Include(a => a.Locus)
                .Include(a => a.ProfileThinhLc)
                .Include(a => a.Test)
                .Where(a => a.ProfileThinhLcid == profileId)
                .ToListAsync();

            return alleleResults ?? new List<AlleleResultsPhienNt>();
        }

        public async Task<PaginationResult<List<AlleleResultsPhienNt>>> SearchAlleleResultsWithPagingAsync(int page, int pageSize)
        {
            var alleleResults = await _context.AlleleResultsPhienNts
                .Include(a => a.Locus)
                .Include(a => a.ProfileThinhLc)
                .Include(a => a.Test)
                .ToListAsync();

            var totalItems = alleleResults.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            alleleResults = alleleResults.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var result = new PaginationResult<List<AlleleResultsPhienNt>>
            {
                TotalItems = totalItems,
                TotalPage = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = alleleResults
            };
            return result;
        }

        public async Task<List<AlleleResultsPhienNt>> GetAlleleResultsByLocusIdAsync(int locusId)
        {
            var alleleResults = await _context.AlleleResultsPhienNts
                .Include(a => a.Locus)
                .Include(a => a.ProfileThinhLc)
                .Include(a => a.Test)
                .Where(a => a.LocusId == locusId)
                .ToListAsync();

            return alleleResults ?? new List<AlleleResultsPhienNt>();
        }
    }
}
