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
    public class UserServiceNhanVtRepository : GenericRepository<UserServiceNhanVt>
    {
        public UserServiceNhanVtRepository() { }

        public UserServiceNhanVtRepository(SE18_PRN232_SE1730_G3_DNATestingSystemContext context) => _context = context;
        
        public async Task<PaginationResult<List<UserServiceNhanVt>>> SearchAsync(int? userId, int? serviceId, string role, bool? isActive, int page, int pageSize)
        {
            var query = _context.UserServiceNhanVts
                .Include(us => us.UserAccount)
                .Include(us => us.ServicesNhanVt)
                .Include(us => us.CreatedByNavigation)
                .Include(us => us.ModifiedByNavigation)
                .AsQueryable();
            
            if (userId.HasValue)
            {
                query = query.Where(us => us.UserAccountId == userId.Value);
            }
            
            if (serviceId.HasValue)
            {
                query = query.Where(us => us.ServicesNhanVtid == serviceId.Value);
            }
            
            if (!string.IsNullOrEmpty(role))
            {
                query = query.Where(us => us.Role != null && us.Role.Contains(role));
            }
            
            if (isActive.HasValue)
            {
                query = query.Where(us => us.IsActive == isActive.Value);
            }
            
            var userServices = await query.ToListAsync();
            var totalItems = userServices.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            
            userServices = userServices.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            
            return new PaginationResult<List<UserServiceNhanVt>>
            {
                TotalItems = totalItems,
                TotalPage = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = userServices
            };
        }
        
        public async Task<List<UserServiceNhanVt>> GetByUserIdAsync(int userId)
        {
            return await _context.UserServiceNhanVts
                .Include(us => us.UserAccount)
                .Include(us => us.ServicesNhanVt)
                .Where(us => us.UserAccountId == userId)
                .ToListAsync();
        }
        
        public async Task<List<UserServiceNhanVt>> GetByServiceIdAsync(int serviceId)
        {
            return await _context.UserServiceNhanVts
                .Include(us => us.UserAccount)
                .Include(us => us.ServicesNhanVt)
                .Where(us => us.ServicesNhanVtid == serviceId)
                .ToListAsync();
        }
        
        public async Task<List<UserServiceNhanVt>> GetActiveAssignmentsAsync()
        {
            return await _context.UserServiceNhanVts
                .Include(us => us.UserAccount)
                .Include(us => us.ServicesNhanVt)
                .Where(us => us.IsActive == true)
                .ToListAsync();
        }
        
        public async Task<List<UserServiceNhanVt>> GetByRoleAsync(string role)
        {
            return await _context.UserServiceNhanVts
                .Include(us => us.UserAccount)
                .Include(us => us.ServicesNhanVt)
                .Where(us => us.Role != null && us.Role.Contains(role))
                .ToListAsync();
        }
        
        public async Task<List<UserServiceNhanVt>> GetActiveServicesAsync()
        {
            return await _context.UserServiceNhanVts
                .Include(us => us.UserAccount)
                .Include(us => us.ServicesNhanVt)
                .Where(us => us.IsActive == true)
                .ToListAsync();
        }
        
        public async Task<bool> DeactivateAsync(int id)
        {
            var userService = await _context.UserServiceNhanVts.FindAsync(id);
            if (userService == null) return false;
            
            userService.IsActive = false;
            userService.ModifiedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }
        
        public async Task<bool> ActivateAsync(int id)
        {
            var userService = await _context.UserServiceNhanVts.FindAsync(id);
            if (userService == null) return false;
            
            userService.IsActive = true;
            userService.ModifiedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserServiceNhanVt>> GetAllUserServicesAsync()
        {
            var allUserServices = await _context.UserServiceNhanVts
                .Include(us => us.UserAccount)
                .Include(us => us.ServicesNhanVt)
                .Include(us => us.CreatedByNavigation)
                .Include(us => us.ModifiedByNavigation)
                .ToListAsync();
            return allUserServices ?? new List<UserServiceNhanVt>();
        }

        public async Task<UserServiceNhanVt> GetUserServiceByIdAsync(int id)
        {
            var userService = await _context.UserServiceNhanVts
                .Include(us => us.UserAccount)
                .Include(us => us.ServicesNhanVt)
                .Include(us => us.CreatedByNavigation)
                .Include(us => us.ModifiedByNavigation)
                .FirstOrDefaultAsync(us => us.UserServiceNhanVtid == id);
            return userService ?? new UserServiceNhanVt();
        }

        public async Task<List<UserServiceNhanVt>> GetUserServicesByUserIdAsync(int userId)
        {
            var userServices = await _context.UserServiceNhanVts
                .Include(us => us.UserAccount)
                .Include(us => us.ServicesNhanVt)
                .Include(us => us.CreatedByNavigation)
                .Include(us => us.ModifiedByNavigation)
                .Where(us => us.UserAccountId == userId)
                .ToListAsync();
            return userServices ?? new List<UserServiceNhanVt>();
        }

        public async Task<List<UserServiceNhanVt>> GetUserServicesByServiceIdAsync(int serviceId)
        {
            var userServices = await _context.UserServiceNhanVts
                .Include(us => us.UserAccount)
                .Include(us => us.ServicesNhanVt)
                .Include(us => us.CreatedByNavigation)
                .Include(us => us.ModifiedByNavigation)
                .Where(us => us.ServicesNhanVtid == serviceId)
                .ToListAsync();
            return userServices ?? new List<UserServiceNhanVt>();
        }

        public async Task<List<UserServiceNhanVt>> GetActiveUserServicesAsync()
        {
            var activeUserServices = await _context.UserServiceNhanVts
                .Include(us => us.UserAccount)
                .Include(us => us.ServicesNhanVt)
                .Include(us => us.CreatedByNavigation)
                .Include(us => us.ModifiedByNavigation)
                .Where(us => us.IsActive == true)
                .ToListAsync();
            return activeUserServices ?? new List<UserServiceNhanVt>();
        }

        public async Task<PaginationResult<List<UserServiceNhanVt>>> SearchUserServicesWithPagingAsync(int? userId, int? serviceId, string role, bool? isActive, int page, int pageSize)
        {
            var userServicesQuery = _context.UserServiceNhanVts
                .Include(us => us.UserAccount)
                .Include(us => us.ServicesNhanVt)
                .Include(us => us.CreatedByNavigation)
                .Include(us => us.ModifiedByNavigation)
                .AsQueryable();

            if (userId.HasValue)
            {
                userServicesQuery = userServicesQuery.Where(us => us.UserAccountId == userId.Value);
            }

            if (serviceId.HasValue)
            {
                userServicesQuery = userServicesQuery.Where(us => us.ServicesNhanVtid == serviceId.Value);
            }

            if (!string.IsNullOrEmpty(role))
            {
                userServicesQuery = userServicesQuery.Where(us => us.Role != null && us.Role.Contains(role));
            }

            if (isActive.HasValue)
            {
                userServicesQuery = userServicesQuery.Where(us => us.IsActive == isActive.Value);
            }

            var userServices = await userServicesQuery.OrderByDescending(us => us.AssignedDate).ToListAsync();
            var totalItems = userServices.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            userServices = userServices.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var result = new PaginationResult<List<UserServiceNhanVt>>
            {
                TotalItems = totalItems,
                TotalPage = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = userServices
            };
            return result;
        }
    }
}
