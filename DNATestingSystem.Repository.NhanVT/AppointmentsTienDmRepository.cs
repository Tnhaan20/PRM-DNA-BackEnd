using DNATestingSystem.Repository.NhanVT.Basic;
using DNATestingSystem.Repository.NhanVT.DBContext;
using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using DNATestingSystem.Repository.NhanVT.Models;
using DNATestingSystem.Repository.TienDM.ModelExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAppointmentsTienDm = DNATestingSystem.Repository.NhanVT.ModelExtensions.SearchAppointmentsTienDm;

namespace DNATestingSystem.Repository.NhanVT
{
    public class AppointmentsTienDmRepository : GenericRepository<AppointmentsTienDm>
    {
        public AppointmentsTienDmRepository() : base() { }

        public AppointmentsTienDmRepository(SE18_PRN232_SE1730_G3_DNATestingSystemContext context) : base(context) { }

        /// <summary>
        /// Override GetAllAsync to include related entities
        /// </summary>
        public new async Task<List<AppointmentsTienDm>> GetAllAsync()
        {
            var appointments = await _context.AppointmentsTienDms
                .ToListAsync();
            return appointments ?? new List<AppointmentsTienDm>();
        }        /// <summary>
                 /// Override GetByIdAsync to include related entities
                 /// </summary>
        public new async Task<AppointmentsTienDm> GetByIdAsync(int id)
        {
            var appointment = await _context.AppointmentsTienDms
                .FirstOrDefaultAsync(a => a.AppointmentsTienDmid == id);
            return appointment ?? new AppointmentsTienDm();
        }

        /// <summary>
        /// Get all appointments with pagination - optimized for large datasets
        /// </summary>
        public async Task<PaginationResult<List<AppointmentsTienDm>>> GetAllPaginatedAsync(int page, int pageSize)
        {
            // Use empty search criteria to get all items with pagination
            return await SearchAsync(0, string.Empty, 0, page, pageSize);
        }

        /// <summary>
        /// Search appointments with individual parameters
        /// </summary>
        public async Task<PaginationResult<List<AppointmentsTienDm>>> SearchAsync(int id, string contactPhone, decimal totalAmount, int page, int pageSize)
        {
            var query = BuildSearchQuery(id, contactPhone, totalAmount);
            return await ExecutePaginatedQuery(query, page, pageSize);
        }

        /// <summary>
        /// Search appointments using SearchRequest model
        /// </summary>
        public async Task<PaginationResult<List<AppointmentsTienDm>>> SearchAsync(SearchAppointmentsTienDm searchRequest)
        {
            // Set default values if null
            var page = searchRequest.CurrentPage ?? 1;
            var pageSize = searchRequest.PageSize ?? 10;
            var contactPhone = searchRequest.ContactPhone;
            var totalAmount = searchRequest.TotalAmount ?? 0;
            var id = searchRequest.AppointmentsTienDmid ?? 0;

            var query = BuildSearchQuery(id, contactPhone, totalAmount);
            return await ExecutePaginatedQuery(query, page, pageSize);
        }
        
        /// <summary>
        /// Builds the base search query with includes and filters
        /// </summary>

        private IQueryable<AppointmentsTienDm> BuildSearchQuery(int id, string? contactPhone, decimal totalAmount)
        {
            return _context.AppointmentsTienDms
                .Where(a => (string.IsNullOrEmpty(contactPhone) || a.ContactPhone.Contains(contactPhone))
                    && (totalAmount == 0 || a.TotalAmount == totalAmount)
                    && (id == 0 || a.AppointmentsTienDmid == id));
        }

        /// <summary>
        /// Executes paginated query and returns PaginationResult
        /// </summary>
        private async Task<PaginationResult<List<AppointmentsTienDm>>> ExecutePaginatedQuery(IQueryable<AppointmentsTienDm> query, int page, int pageSize)
        {
            // Get total count for pagination
            var totalItems = await query.CountAsync();

            // Calculate total pages
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Apply pagination
            var appointments = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginationResult<List<AppointmentsTienDm>>
            {
                TotalItems = totalItems,
                TotalPage = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = appointments ?? new List<AppointmentsTienDm>()
            };
        }/// <summary>

         /// Override CreateAsync to set CreatedDate automatically
         /// </summary>

        public new async Task<int> CreateAsync(AppointmentsTienDm entity)
        {
            if (entity.CreatedDate == null)
                entity.CreatedDate = DateTime.Now;

            return await base.CreateAsync(entity);
        }

        /// <summary>
        /// Override UpdateAsync to set ModifiedDate automatically
        /// </summary>
        public new async Task<int> UpdateAsync(AppointmentsTienDm entity)
        {
            entity.ModifiedDate = DateTime.Now;
            return await base.UpdateAsync(entity);
        }

        /// <summary>
        /// Delete appointment by ID - leverages base class RemoveAsync
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            var appointment = await _context.AppointmentsTienDms.FindAsync(id);
            if (appointment == null)
                return false;

            return await base.RemoveAsync(appointment);
        }

        /// <summary>
        /// Create appointment using DTO to avoid navigation property validation issues
        /// </summary>
        public async Task<int> CreateFromDtoAsync(CreateAppointmentsTienDmDto createDto)
        {
            var entity = new AppointmentsTienDm
            {
                UserAccountId = createDto.UserAccountId,
                ServicesNhanVtid = createDto.ServicesNhanVtid,
                AppointmentStatusesTienDmid = createDto.AppointmentStatusesTienDmid,
                AppointmentDate = createDto.AppointmentDate,
                AppointmentTime = createDto.AppointmentTime,
                SamplingMethod = createDto.SamplingMethod,
                Address = createDto.Address,
                ContactPhone = createDto.ContactPhone,
                Notes = createDto.Notes,
                TotalAmount = createDto.TotalAmount,
                IsPaid = createDto.IsPaid,
                CreatedDate = DateTime.Now
            };

            return await base.CreateAsync(entity);
        }

        /// <summary>
        /// Update appointment using DTO to avoid navigation property validation issues
        /// </summary>
        public async Task<int> UpdateFromDtoAsync(UpdateAppointmentsTienDmDto updateDto)
        {
            var entity = new AppointmentsTienDm
            {
                AppointmentsTienDmid = updateDto.AppointmentsTienDmid,
                UserAccountId = updateDto.UserAccountId,
                ServicesNhanVtid = updateDto.ServicesNhanVtid,
                AppointmentStatusesTienDmid = updateDto.AppointmentStatusesTienDmid,
                AppointmentDate = updateDto.AppointmentDate,
                AppointmentTime = updateDto.AppointmentTime,
                SamplingMethod = updateDto.SamplingMethod,
                Address = updateDto.Address,
                ContactPhone = updateDto.ContactPhone,
                Notes = updateDto.Notes,
                TotalAmount = updateDto.TotalAmount,
                IsPaid = updateDto.IsPaid,
                ModifiedDate = DateTime.Now
            };

            return await base.UpdateAsync(entity);
        }

        /// <summary>
        /// Get appointment with related data for display
        /// </summary>
        public async Task<AppointmentsTienDmDisplayDto?> GetDisplayDtoByIdAsync(int id)
        {
            var appointment = await _context.AppointmentsTienDms
                .Include(a => a.AppointmentStatusesTienDm)
                .Include(a => a.ServicesNhanVt)
                .Include(a => a.UserAccount)
                .FirstOrDefaultAsync(a => a.AppointmentsTienDmid == id);

            if (appointment == null)
                return null;

            return new AppointmentsTienDmDisplayDto
            {
                AppointmentsTienDmid = appointment.AppointmentsTienDmid,
                UserAccountId = appointment.UserAccountId,
                ServicesNhanVtid = appointment.ServicesNhanVtid,
                AppointmentStatusesTienDmid = appointment.AppointmentStatusesTienDmid,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                SamplingMethod = appointment.SamplingMethod,
                Address = appointment.Address,
                ContactPhone = appointment.ContactPhone,
                Notes = appointment.Notes,
                CreatedDate = appointment.CreatedDate,
                ModifiedDate = appointment.ModifiedDate,
                TotalAmount = appointment.TotalAmount,
                IsPaid = appointment.IsPaid,
                StatusName = appointment.AppointmentStatusesTienDm?.StatusName,
                ServiceName = appointment.ServicesNhanVt?.ServiceName,
                UserName = appointment.UserAccount?.FullName,
                UserEmail = appointment.UserAccount?.Email
            };
        }

        /// <summary>
        /// Get paginated appointments as display DTOs
        /// </summary>
        public async Task<PaginationResult<List<AppointmentsTienDmDisplayDto>>> GetDisplayDtosPaginatedAsync(SearchAppointmentsTienDm searchRequest)
        {
            var page = searchRequest.CurrentPage ?? 1;
            var pageSize = searchRequest.PageSize ?? 10;

            var query = _context.AppointmentsTienDms
                .Include(a => a.AppointmentStatusesTienDm)
                .Include(a => a.ServicesNhanVt)
                .Include(a => a.UserAccount)
                .Where(a => (string.IsNullOrEmpty(searchRequest.ContactPhone) || a.ContactPhone.Contains(searchRequest.ContactPhone))
                    && (searchRequest.TotalAmount == null || a.TotalAmount == searchRequest.TotalAmount)
                    && (searchRequest.AppointmentsTienDmid == null || a.AppointmentsTienDmid == searchRequest.AppointmentsTienDmid));

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var appointments = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new AppointmentsTienDmDisplayDto
                {
                    AppointmentsTienDmid = a.AppointmentsTienDmid,
                    UserAccountId = a.UserAccountId,
                    ServicesNhanVtid = a.ServicesNhanVtid,
                    AppointmentStatusesTienDmid = a.AppointmentStatusesTienDmid,
                    AppointmentDate = a.AppointmentDate,
                    AppointmentTime = a.AppointmentTime,
                    SamplingMethod = a.SamplingMethod,
                    Address = a.Address,
                    ContactPhone = a.ContactPhone,
                    Notes = a.Notes,
                    CreatedDate = a.CreatedDate,
                    ModifiedDate = a.ModifiedDate,
                    TotalAmount = a.TotalAmount,
                    IsPaid = a.IsPaid,
                    StatusName = a.AppointmentStatusesTienDm != null ? a.AppointmentStatusesTienDm.StatusName : null,
                    ServiceName = a.ServicesNhanVt != null ? a.ServicesNhanVt.ServiceName : null,
                    UserName = a.UserAccount != null ? a.UserAccount.FullName : null,
                    UserEmail = a.UserAccount != null ? a.UserAccount.Email : null
                })
                .ToListAsync();

            return new PaginationResult<List<AppointmentsTienDmDisplayDto>>
            {
                TotalItems = totalItems,
                TotalPage = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = appointments
            };
        }
    }
}
