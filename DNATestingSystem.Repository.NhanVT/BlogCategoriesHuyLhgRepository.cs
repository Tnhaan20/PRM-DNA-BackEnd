using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNATestingSystem.Repository.NhanVT.Basic;
using DNATestingSystem.Repository.NhanVT.DBContext;
using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using DNATestingSystem.Repository.NhanVT.Models;
using Microsoft.EntityFrameworkCore;

namespace DNATestingSystem.Repository.NhanVT
{
    public class BlogCategoriesHuyLhgRepository : GenericRepository<BlogCategoriesHuyLhg>
    {
        public BlogCategoriesHuyLhgRepository() { }

        public BlogCategoriesHuyLhgRepository(SE18_PRN232_SE1730_G3_DNATestingSystemContext context) => _context = context;

        public async Task<List<BlogCategoriesHuyLhg>> GetAllBlogCategoriesAsync()
        {
            var allCategories = await _context.BlogCategoriesHuyLhgs.ToListAsync();
            return allCategories ?? new List<BlogCategoriesHuyLhg>();
        }

        public async Task<BlogCategoriesHuyLhg> GetBlogCategoryByIdAsync(int id)
        {
            var category = await _context.BlogCategoriesHuyLhgs
                .FirstOrDefaultAsync(bc => bc.BlogCategoryHuyLhgid == id);
            return category ?? new BlogCategoriesHuyLhg();
        }

        public async Task<List<BlogCategoriesHuyLhg>> GetActiveBlogCategoriesAsync()
        {
            var activeCategories = await _context.BlogCategoriesHuyLhgs
                .Where(bc => bc.IsActive == true)
                .ToListAsync();
            return activeCategories ?? new List<BlogCategoriesHuyLhg>();
        }
        
        public async Task<List<BlogCategoriesHuyLhg>> GetActiveAsync()
        {
            var activeCategories = await _context.BlogCategoriesHuyLhgs
                .Where(bc => bc.IsActive == true)
                .ToListAsync();
            return activeCategories ?? new List<BlogCategoriesHuyLhg>();
        }

        public async Task<PaginationResult<List<BlogCategoriesHuyLhg>>> SearchBlogCategoriesWithPagingAsync(string categoryName, bool? isActive, int page, int pageSize)
        {
            var categoriesQuery = _context.BlogCategoriesHuyLhgs.AsQueryable();

            if (!string.IsNullOrEmpty(categoryName))
            {
                categoriesQuery = categoriesQuery.Where(bc => bc.CategoryName.Contains(categoryName));
            }

            if (isActive.HasValue)
            {
                categoriesQuery = categoriesQuery.Where(bc => bc.IsActive == isActive.Value);
            }

            var categories = await categoriesQuery.ToListAsync();
            var totalItems = categories.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            categories = categories.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var result = new PaginationResult<List<BlogCategoriesHuyLhg>>
            {
                TotalItems = totalItems,
                TotalPage = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = categories
            };
            return result;
        }
    }
}
