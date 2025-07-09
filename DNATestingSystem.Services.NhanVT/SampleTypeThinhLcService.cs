using DNATestingSystem.Repository.NhanVT.Models;
using DNATestingSystem.Repository.NhanVT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATestingSystem.Services.NhanVT
{
    public class SampleTypeThinhLcService : ISampleTypeThinhLcService
    {
        private readonly SampleTypeThinhLcRepository _repository;

        public SampleTypeThinhLcService()
            => _repository = new SampleTypeThinhLcRepository();

        public async Task<List<SampleTypeThinhLc>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<SampleTypeThinhLc> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<SampleTypeThinhLc> GetByTypeNameAsync(string typeName)
        {
            return await _repository.GetByTypeNameAsync(typeName);
        }

        public async Task<List<SampleTypeThinhLc>> GetActiveAsync()
        {
            return await _repository.GetActiveAsync();
        }
        
        public async Task<List<SampleTypeThinhLc>> GetActiveTypesAsync()
        {
            return await _repository.GetActiveAsync();
        }

        public async Task<int> CreateAsync(SampleTypeThinhLc entity)
        {
            return await _repository.CreateAsync(entity);
        }

        public async Task<int> UpdateAsync(SampleTypeThinhLc entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.SoftDeleteAsync(id);
        }

        public async Task<bool> ActivateAsync(int id)
        {
            return await _repository.ActivateAsync(id);
        }

        public async Task<bool> DeactivateAsync(int id)
        {
            return await _repository.DeactivateAsync(id);
        }
    }
}
