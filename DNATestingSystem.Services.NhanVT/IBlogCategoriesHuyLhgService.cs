using DNATestingSystem.Repository.NhanVT.Models;
using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATestingSystem.Services.NhanVT
{
    public interface IBlogCategoriesHuyLhgService
    {
        Task<List<BlogCategoriesHuyLhg>> GetAllAsync();
        Task<BlogCategoriesHuyLhg> GetByIdAsync(int id);
        Task<List<BlogCategoriesHuyLhg>> GetActiveCategoriesAsync();
        Task<List<BlogCategoriesHuyLhg>> GetActiveAsync();
        Task<int> CreateAsync(BlogCategoriesHuyLhg entity);
        Task<int> UpdateAsync(BlogCategoriesHuyLhg entity);
        Task<bool> DeleteAsync(int id);
    }
}
