using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using DNATestingSystem.Repository.NhanVT.Models;

namespace DNATestingSystem.Services.NhanVT
{
    public interface ILocusMatchResultsPhienNtService
    {
        Task<List<LocusMatchResultsPhienNt>> GetAllLocusMatchResultsAsync();
        Task<LocusMatchResultsPhienNt> GetLocusMatchResultByIdAsync(int id);
        Task<List<LocusMatchResultsPhienNt>> GetLocusMatchResultsByTestIdAsync(int testId);
        Task<List<LocusMatchResultsPhienNt>> GetLocusMatchResultsByLocusIdAsync(int locusId);
        Task<int> CreateLocusMatchResultAsync(LocusMatchResultsPhienNt locusMatchResult);
        Task<int> UpdateLocusMatchResultAsync(LocusMatchResultsPhienNt locusMatchResult);
        Task<bool> DeleteLocusMatchResultAsync(int id);
        Task<PaginationResult<List<LocusMatchResultsPhienNt>>> SearchLocusMatchResultsWithPagingAsync(int? testId, int? locusId, int page, int pageSize);
    }
}
