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
    public class BlogsHuyLhgService : IBlogsHuyLhgService
    {
        private readonly BlogsHuyLhgRepository _repository;

        public BlogsHuyLhgService()
            => _repository = new BlogsHuyLhgRepository();

        public async Task<List<BlogsHuyLhg>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<BlogsHuyLhg> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<BlogsHuyLhg>> GetByCategoryIdAsync(int categoryId)
        {
            return await _repository.GetByCategoryIdAsync(categoryId);
        }

        public async Task<List<BlogsHuyLhg>> GetByUserIdAsync(int userId)
        {
            return await _repository.GetByUserIdAsync(userId);
        }

        public async Task<List<BlogsHuyLhg>> GetPublishedAsync()
        {
            return await _repository.GetPublishedAsync();
        }

        public async Task<PaginationResult<List<BlogsHuyLhg>>> SearchAsync(string title, int? categoryId, int page, int pageSize)
        {
            return await _repository.SearchAsync(title, categoryId, page, pageSize);
        }

        public async Task<int> CreateAsync(BlogsHuyLhg entity)
        {
            return await _repository.CreateAsync(entity);
        }

        public async Task<int> UpdateAsync(BlogsHuyLhg entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;
            return await _repository.RemoveAsync(entity);
        }

        public async Task<bool> PublishAsync(int id)
        {
            return await _repository.PublishAsync(id);
        }
    }
}
