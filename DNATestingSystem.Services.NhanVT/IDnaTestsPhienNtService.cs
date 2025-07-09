using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using DNATestingSystem.Repository.NhanVT.Models;

namespace DNATestingSystem.Services.NhanVT
{
    public interface IDnaTestsPhienNtService
    {
        Task<List<DnaTestsPhienNt>> GetAllDnaTestsAsync();
        Task<DnaTestsPhienNt> GetDnaTestByIdAsync(int id);
        Task<List<DnaTestsPhienNt>> GetCompletedDnaTestsAsync();
        Task<List<DnaTestsPhienNt>> GetDnaTestsByTypeAsync(string testType);
        Task<int> CreateDnaTestAsync(DnaTestsPhienNt dnaTest);
        Task<int> UpdateDnaTestAsync(DnaTestsPhienNt dnaTest);
        Task<bool> DeleteDnaTestAsync(int id);
        Task<PaginationResult<List<DnaTestsPhienNt>>> SearchDnaTestsWithPagingAsync(string testType, bool? isCompleted, int page, int pageSize);
    }
}
