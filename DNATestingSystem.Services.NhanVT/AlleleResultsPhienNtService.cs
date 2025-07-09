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
    public class AlleleResultsPhienNtService : IAlleleResultsPhienNtService
    {
        private readonly AlleleResultsPhienNtRepository _repository;

        public AlleleResultsPhienNtService()
            => _repository = new AlleleResultsPhienNtRepository();

        // New methods implementing the interface
        public async Task<List<AlleleResultsPhienNt>> GetAllAsync()
        {
            return await _repository.GetAllAlleleResultsAsync();
        }

        public async Task<AlleleResultsPhienNt> GetByIdAsync(int id)
        {
            return await _repository.GetAlleleResultByIdAsync(id);
        }

        public async Task<List<AlleleResultsPhienNt>> GetByTestIdAsync(int testId)
        {
            return await _repository.GetAlleleResultsByTestIdAsync(testId);
        }

        public async Task<List<AlleleResultsPhienNt>> GetByProfileIdAsync(int profileId)
        {
            return await _repository.GetAlleleResultsByProfileIdAsync(profileId);
        }
        
        public async Task<List<AlleleResultsPhienNt>> GetByLocusIdAsync(int locusId)
        {
            return await _repository.GetAlleleResultsByLocusIdAsync(locusId);
        }

        public async Task<int> CreateAsync(AlleleResultsPhienNt alleleResult)
        {
            return await _repository.CreateAsync(alleleResult);
        }

        public async Task<int> UpdateAsync(AlleleResultsPhienNt alleleResult)
        {
            return await _repository.UpdateAsync(alleleResult);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var alleleResult = await _repository.GetByIdAsync(id);
            if (alleleResult != null)
            {
                return await _repository.RemoveAsync(alleleResult);
            }
            return false;
        }

        // Original methods
        public async Task<List<AlleleResultsPhienNt>> GetAllAlleleResultsAsync()
        {
            return await _repository.GetAllAlleleResultsAsync();
        }

        public async Task<AlleleResultsPhienNt> GetAlleleResultByIdAsync(int id)
        {
            return await _repository.GetAlleleResultByIdAsync(id);
        }

        public async Task<List<AlleleResultsPhienNt>> GetAlleleResultsByTestIdAsync(int testId)
        {
            return await _repository.GetAlleleResultsByTestIdAsync(testId);
        }

        public async Task<List<AlleleResultsPhienNt>> GetAlleleResultsByProfileIdAsync(int profileId)
        {
            return await _repository.GetAlleleResultsByProfileIdAsync(profileId);
        }

        public async Task<int> CreateAlleleResultAsync(AlleleResultsPhienNt alleleResult)
        {
            return await _repository.CreateAsync(alleleResult);
        }

        public async Task<int> UpdateAlleleResultAsync(AlleleResultsPhienNt alleleResult)
        {
            return await _repository.UpdateAsync(alleleResult);
        }

        public async Task<bool> DeleteAlleleResultAsync(int id)
        {
            var alleleResult = await _repository.GetByIdAsync(id);
            if (alleleResult != null)
            {
                return await _repository.RemoveAsync(alleleResult);
            }
            return false;
        }

        public async Task<PaginationResult<List<AlleleResultsPhienNt>>> SearchAlleleResultsWithPagingAsync(int page, int pageSize)
        {
            return await _repository.SearchAlleleResultsWithPagingAsync(page, pageSize);
        }
    }
}
