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
    public class LocusMatchResultsPhienNtService : ILocusMatchResultsPhienNtService
    {
        private readonly LocusMatchResultsPhienNtRepository _repository;

        public LocusMatchResultsPhienNtService()
            => _repository = new LocusMatchResultsPhienNtRepository();

        public async Task<List<LocusMatchResultsPhienNt>> GetAllLocusMatchResultsAsync()
        {
            return await _repository.GetAllLocusMatchResultsAsync();
        }

        public async Task<LocusMatchResultsPhienNt> GetLocusMatchResultByIdAsync(int id)
        {
            return await _repository.GetLocusMatchResultByIdAsync(id);
        }

        public async Task<List<LocusMatchResultsPhienNt>> GetLocusMatchResultsByTestIdAsync(int testId)
        {
            return await _repository.GetLocusMatchResultsByTestIdAsync(testId);
        }

        public async Task<List<LocusMatchResultsPhienNt>> GetLocusMatchResultsByLocusIdAsync(int locusId)
        {
            return await _repository.GetLocusMatchResultsByLocusIdAsync(locusId);
        }

        public async Task<int> CreateLocusMatchResultAsync(LocusMatchResultsPhienNt locusMatchResult)
        {
            return await _repository.CreateAsync(locusMatchResult);
        }

        public async Task<int> UpdateLocusMatchResultAsync(LocusMatchResultsPhienNt locusMatchResult)
        {
            return await _repository.UpdateAsync(locusMatchResult);
        }

        public async Task<bool> DeleteLocusMatchResultAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;
            return await _repository.RemoveAsync(entity);
        }

        public async Task<PaginationResult<List<LocusMatchResultsPhienNt>>> SearchLocusMatchResultsWithPagingAsync(int? testId, int? locusId, int page, int pageSize)
        {
            return await _repository.SearchLocusMatchResultsWithPagingAsync(testId, locusId, page, pageSize);
        }
    }
}
