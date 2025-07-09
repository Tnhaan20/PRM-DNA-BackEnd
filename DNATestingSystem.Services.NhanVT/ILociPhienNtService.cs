using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using DNATestingSystem.Repository.NhanVT.Models;

namespace DNATestingSystem.Services.NhanVT
{
    public interface ILociPhienNtService
    {
        Task<List<LociPhienNt>> GetAllLociAsync();
        Task<LociPhienNt> GetLocusByIdAsync(int id);
        Task<LociPhienNt> GetLocusByNameAsync(string name);
        Task<List<LociPhienNt>> GetCodisLociAsync();
        Task<int> CreateLocusAsync(LociPhienNt locus);
        Task<int> UpdateLocusAsync(LociPhienNt locus);
        Task<bool> DeleteLocusAsync(int id);
        Task<PaginationResult<List<LociPhienNt>>> SearchLociWithPagingAsync(string name, bool? isCodis, int page, int pageSize);
    }
}
