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
    public class LociPhienNtService : ILociPhienNtService
    {
        private readonly LociPhienNtRepository _repository;

        public LociPhienNtService()
            => _repository = new LociPhienNtRepository();

        public async Task<List<LociPhienNt>> GetAllLociAsync()
        {
            return await _repository.GetAllLociAsync();
        }

        public async Task<LociPhienNt> GetLocusByIdAsync(int id)
        {
            return await _repository.GetLocusByIdAsync(id);
        }

        public async Task<LociPhienNt> GetLocusByNameAsync(string name)
        {
            return await _repository.GetLocusByNameAsync(name);
        }

        public async Task<List<LociPhienNt>> GetCodisLociAsync()
        {
            return await _repository.GetCodisLociAsync();
        }

        public async Task<int> CreateLocusAsync(LociPhienNt locus)
        {
            return await _repository.CreateAsync(locus);
        }

        public async Task<int> UpdateLocusAsync(LociPhienNt locus)
        {
            return await _repository.UpdateAsync(locus);
        }

        public async Task<bool> DeleteLocusAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;
            return await _repository.RemoveAsync(entity);
        }

        public async Task<PaginationResult<List<LociPhienNt>>> SearchLociWithPagingAsync(string name, bool? isCodis, int page, int pageSize)
        {
            return await _repository.SearchLociWithPagingAsync(name, isCodis, page, pageSize);
        }
    }
}
