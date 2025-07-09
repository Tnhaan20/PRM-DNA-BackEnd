using DNATestingSystem.Repository.NhanVT.Models;
using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATestingSystem.Services.NhanVT
{
    public interface ISampleTypeThinhLcService
    {
        Task<List<SampleTypeThinhLc>> GetAllAsync();
        Task<SampleTypeThinhLc> GetByIdAsync(int id);
        Task<SampleTypeThinhLc> GetByTypeNameAsync(string typeName);
        Task<List<SampleTypeThinhLc>> GetActiveAsync();
        Task<List<SampleTypeThinhLc>> GetActiveTypesAsync();
        Task<int> CreateAsync(SampleTypeThinhLc entity);
        Task<int> UpdateAsync(SampleTypeThinhLc entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> ActivateAsync(int id);
        Task<bool> DeactivateAsync(int id);
    }
}
