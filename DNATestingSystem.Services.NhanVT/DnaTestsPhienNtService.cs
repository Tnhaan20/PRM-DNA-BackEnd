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
    public class DnaTestsPhienNtService : IDnaTestsPhienNtService
    {
        private readonly DnaTestsPhienNtRepository _repository;

        public DnaTestsPhienNtService()
            => _repository = new DnaTestsPhienNtRepository();

        public async Task<List<DnaTestsPhienNt>> GetAllDnaTestsAsync()
        {
            return await _repository.GetAllDnaTestsAsync();
        }

        public async Task<DnaTestsPhienNt> GetDnaTestByIdAsync(int id)
        {
            return await _repository.GetDnaTestByIdAsync(id);
        }

        public async Task<List<DnaTestsPhienNt>> GetCompletedDnaTestsAsync()
        {
            return await _repository.GetCompletedDnaTestsAsync();
        }

        public async Task<List<DnaTestsPhienNt>> GetDnaTestsByTypeAsync(string testType)
        {
            return await _repository.GetDnaTestsByTypeAsync(testType);
        }

        public async Task<int> CreateDnaTestAsync(DnaTestsPhienNt dnaTest)
        {
            return await _repository.CreateAsync(dnaTest);
        }

        public async Task<int> UpdateDnaTestAsync(DnaTestsPhienNt dnaTest)
        {
            return await _repository.UpdateAsync(dnaTest);
        }

        public async Task<bool> DeleteDnaTestAsync(int id)
        {
            var dnaTest = await _repository.GetByIdAsync(id);
            if (dnaTest != null)
            {
                return await _repository.RemoveAsync(dnaTest);
            }
            return false;
        }

        public async Task<PaginationResult<List<DnaTestsPhienNt>>> SearchDnaTestsWithPagingAsync(string testType, bool? isCompleted, int page, int pageSize)
        {
            return await _repository.SearchDnaTestsWithPagingAsync(testType, isCompleted, page, pageSize);
        }
    }
}
