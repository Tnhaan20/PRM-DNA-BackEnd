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
    public class DnaTestsPhienNtRepository : GenericRepository<DnaTestsPhienNt>
    {
        public DnaTestsPhienNtRepository() { }

        public DnaTestsPhienNtRepository(SE18_PRN232_SE1730_G3_DNATestingSystemContext context) => _context = context;

        public async Task<List<DnaTestsPhienNt>> GetAllDnaTestsAsync()
        {
            var allDnaTests = await _context.DnaTestsPhienNts.ToListAsync();
            return allDnaTests ?? new List<DnaTestsPhienNt>();
        }

        public async Task<DnaTestsPhienNt> GetDnaTestByIdAsync(int id)
        {
            var dnaTest = await _context.DnaTestsPhienNts
                .FirstOrDefaultAsync(d => d.PhienNtid == id);
            return dnaTest ?? new DnaTestsPhienNt();
        }

        public async Task<List<DnaTestsPhienNt>> GetCompletedDnaTestsAsync()
        {
            var completedTests = await _context.DnaTestsPhienNts
                .Where(d => d.IsCompleted == true)
                .ToListAsync();
            return completedTests ?? new List<DnaTestsPhienNt>();
        }

        public async Task<List<DnaTestsPhienNt>> GetDnaTestsByTypeAsync(string testType)
        {
            var testsByType = await _context.DnaTestsPhienNts
                .Where(d => d.TestType == testType)
                .ToListAsync();
            return testsByType ?? new List<DnaTestsPhienNt>();
        }

        public async Task<PaginationResult<List<DnaTestsPhienNt>>> SearchDnaTestsWithPagingAsync(string testType, bool? isCompleted, int page, int pageSize)
        {
            var testsQuery = _context.DnaTestsPhienNts.AsQueryable();

            if (!string.IsNullOrEmpty(testType))
            {
                testsQuery = testsQuery.Where(d => d.TestType.Contains(testType));
            }

            if (isCompleted.HasValue)
            {
                testsQuery = testsQuery.Where(d => d.IsCompleted == isCompleted.Value);
            }

            var tests = await testsQuery.ToListAsync();
            var totalItems = tests.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            tests = tests.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var result = new PaginationResult<List<DnaTestsPhienNt>>
            {
                TotalItems = totalItems,
                TotalPage = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = tests
            };
            return result;
        }
    }
}
