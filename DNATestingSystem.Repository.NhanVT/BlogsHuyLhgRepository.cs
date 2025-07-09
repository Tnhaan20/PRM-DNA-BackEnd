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
    public class BlogsHuyLhgRepository : GenericRepository<BlogsHuyLhg>
    {
        public BlogsHuyLhgRepository() { }

        public BlogsHuyLhgRepository(SE18_PRN232_SE1730_G3_DNATestingSystemContext context) => _context = context;

        public async Task<List<BlogsHuyLhg>> GetAllBlogsAsync()
        {
            var allBlogs = await _context.BlogsHuyLhgs
                .Include(b => b.BlogCategoryHuyLhg)
                .Include(b => b.User)
                .ToListAsync();
            return allBlogs ?? new List<BlogsHuyLhg>();
        }

        public async Task<BlogsHuyLhg> GetBlogByIdAsync(int id)
        {
            var blog = await _context.BlogsHuyLhgs
                .Include(b => b.BlogCategoryHuyLhg)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.BlogsHuyLhgid == id);
            return blog ?? new BlogsHuyLhg();
        }

        public async Task<List<BlogsHuyLhg>> GetBlogsByCategoryIdAsync(int categoryId)
        {
            var blogs = await _context.BlogsHuyLhgs
                .Include(b => b.BlogCategoryHuyLhg)
                .Include(b => b.User)
                .Where(b => b.BlogCategoryHuyLhgid == categoryId)
                .ToListAsync();
            return blogs ?? new List<BlogsHuyLhg>();
        }

        public async Task<List<BlogsHuyLhg>> GetPublishedBlogsAsync()
        {
            var publishedBlogs = await _context.BlogsHuyLhgs
                .Include(b => b.BlogCategoryHuyLhg)
                .Include(b => b.User)
                .Where(b => b.IsPublished == true)
                .OrderByDescending(b => b.PublishDate)
                .ToListAsync();
            return publishedBlogs ?? new List<BlogsHuyLhg>();
        }

        public async Task<List<BlogsHuyLhg>> GetBlogsByUserIdAsync(int userId)
        {
            var userBlogs = await _context.BlogsHuyLhgs
                .Include(b => b.BlogCategoryHuyLhg)
                .Include(b => b.User)
                .Where(b => b.UserId == userId)
                .ToListAsync();
            return userBlogs ?? new List<BlogsHuyLhg>();
        }

        public async Task<PaginationResult<List<BlogsHuyLhg>>> SearchBlogsWithPagingAsync(string title, int? categoryId, bool? isPublished, int page, int pageSize)
        {
            var blogsQuery = _context.BlogsHuyLhgs
                .Include(b => b.BlogCategoryHuyLhg)
                .Include(b => b.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                blogsQuery = blogsQuery.Where(b => b.Title.Contains(title));
            }

            if (categoryId.HasValue)
            {
                blogsQuery = blogsQuery.Where(b => b.BlogCategoryHuyLhgid == categoryId.Value);
            }

            if (isPublished.HasValue)
            {
                blogsQuery = blogsQuery.Where(b => b.IsPublished == isPublished.Value);
            }

            var blogs = await blogsQuery.OrderByDescending(b => b.CreatedDate).ToListAsync();
            var totalItems = blogs.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            blogs = blogs.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var result = new PaginationResult<List<BlogsHuyLhg>>
            {
                TotalItems = totalItems,
                TotalPage = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = blogs
            };
            return result;
        }

        public async Task<PaginationResult<List<BlogsHuyLhg>>> SearchAsync(string title, int? categoryId, int page, int pageSize)
        {
            var blogsQuery = _context.BlogsHuyLhgs
                .Include(b => b.BlogCategoryHuyLhg)
                .Include(b => b.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                blogsQuery = blogsQuery.Where(b => b.Title.Contains(title));
            }

            if (categoryId.HasValue)
            {
                blogsQuery = blogsQuery.Where(b => b.BlogCategoryHuyLhgid == categoryId.Value);
            }

            var blogs = await blogsQuery.OrderByDescending(b => b.CreatedDate).ToListAsync();
            var totalItems = blogs.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            blogs = blogs.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var result = new PaginationResult<List<BlogsHuyLhg>>
            {
                TotalItems = totalItems,
                TotalPage = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = blogs
            };
            return result;
        }

        public async Task<bool> PublishAsync(int id)
        {
            var blog = await _context.BlogsHuyLhgs.FindAsync(id);
            if (blog == null) return false;

            blog.IsPublished = true;
            blog.PublishDate = DateTime.Now;
            blog.ModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<BlogsHuyLhg>> GetPublishedAsync()
        {
            return await _context.BlogsHuyLhgs
                .Include(b => b.BlogCategoryHuyLhg)
                .Include(b => b.User)
                .Where(b => b.IsPublished)
                .OrderByDescending(b => b.PublishDate)
                .ToListAsync();
        }

        public async Task<List<BlogsHuyLhg>> GetByUserIdAsync(int userId)
        {
            return await _context.BlogsHuyLhgs
                .Include(b => b.BlogCategoryHuyLhg)
                .Include(b => b.User)
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<BlogsHuyLhg>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.BlogsHuyLhgs
                .Include(b => b.BlogCategoryHuyLhg)
                .Include(b => b.User)
                .Where(b => b.BlogCategoryHuyLhgid == categoryId)
                .ToListAsync();
        }
    }
}
