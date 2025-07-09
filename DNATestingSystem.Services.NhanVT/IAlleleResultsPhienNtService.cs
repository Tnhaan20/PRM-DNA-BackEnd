using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using DNATestingSystem.Repository.NhanVT.Models;

namespace DNATestingSystem.Services.NhanVT
{
    public interface IAlleleResultsPhienNtService
    {
        Task<List<AlleleResultsPhienNt>> GetAllAsync();
        Task<AlleleResultsPhienNt> GetByIdAsync(int id);
        Task<List<AlleleResultsPhienNt>> GetByTestIdAsync(int testId);
        Task<List<AlleleResultsPhienNt>> GetByProfileIdAsync(int profileId);
        Task<List<AlleleResultsPhienNt>> GetByLocusIdAsync(int locusId);
        Task<int> CreateAsync(AlleleResultsPhienNt alleleResult);
        Task<int> UpdateAsync(AlleleResultsPhienNt alleleResult);
        Task<bool> DeleteAsync(int id);
        
        // Keep original methods for backward compatibility
        Task<List<AlleleResultsPhienNt>> GetAllAlleleResultsAsync();
        Task<AlleleResultsPhienNt> GetAlleleResultByIdAsync(int id);
        Task<List<AlleleResultsPhienNt>> GetAlleleResultsByTestIdAsync(int testId);
        Task<List<AlleleResultsPhienNt>> GetAlleleResultsByProfileIdAsync(int profileId);
        Task<int> CreateAlleleResultAsync(AlleleResultsPhienNt alleleResult);
        Task<int> UpdateAlleleResultAsync(AlleleResultsPhienNt alleleResult);
        Task<bool> DeleteAlleleResultAsync(int id);
        Task<PaginationResult<List<AlleleResultsPhienNt>>> SearchAlleleResultsWithPagingAsync(int page, int pageSize);
    }
}
