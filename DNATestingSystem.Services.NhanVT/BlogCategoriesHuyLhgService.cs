using DNATestingSystem.Repository.NhanVT.Models;
using DNATestingSystem.Repository.NhanVT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATestingSystem.Services.NhanVT
{
    public class BlogCategoriesHuyLhgService : IBlogCategoriesHuyLhgService
    {
        private readonly BlogCategoriesHuyLhgRepository _repository;

        public BlogCategoriesHuyLhgService()
            => _repository = new BlogCategoriesHuyLhgRepository();

        public async Task<List<BlogCategoriesHuyLhg>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<BlogCategoriesHuyLhg> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<BlogCategoriesHuyLhg>> GetActiveAsync()
        {
            return await _repository.GetActiveAsync();
        }
        
        public async Task<List<BlogCategoriesHuyLhg>> GetActiveCategoriesAsync()
        {
            return await _repository.GetActiveAsync();
        }

        public async Task<int> CreateAsync(BlogCategoriesHuyLhg entity)
        {
            return await _repository.CreateAsync(entity);
        }

        public async Task<int> UpdateAsync(BlogCategoriesHuyLhg entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;
            return await _repository.RemoveAsync(entity);
        }
    }
}
