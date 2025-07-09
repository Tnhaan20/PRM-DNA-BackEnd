using DNATestingSystem.Repository.NhanVT.Models;
using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATestingSystem.Services.NhanVT
{
    public interface IBlogsHuyLhgService
    {
        Task<List<BlogsHuyLhg>> GetAllAsync();
        Task<BlogsHuyLhg> GetByIdAsync(int id);
        Task<List<BlogsHuyLhg>> GetByCategoryIdAsync(int categoryId);
        Task<List<BlogsHuyLhg>> GetByUserIdAsync(int userId);
        Task<List<BlogsHuyLhg>> GetPublishedAsync();
        Task<PaginationResult<List<BlogsHuyLhg>>> SearchAsync(string title, int? categoryId, int page, int pageSize);
        Task<int> CreateAsync(BlogsHuyLhg entity);
        Task<int> UpdateAsync(BlogsHuyLhg entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> PublishAsync(int id);
    }
}
