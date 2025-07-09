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
    public class OrderGiapHdRepository : GenericRepository<OrderGiapHd>
    {
        public OrderGiapHdRepository() { }

        public OrderGiapHdRepository(SE18_PRN232_SE1730_G3_DNATestingSystemContext context) => _context = context;

        public async Task<List<OrderGiapHd>> GetAllOrdersAsync()
        {
            var allOrders = await _context.OrderGiapHds
                .Include(o => o.UserAccount)
                .ToListAsync();
            return allOrders ?? new List<OrderGiapHd>();
        }

        public async Task<OrderGiapHd> GetOrderByIdAsync(int id)
        {
            var order = await _context.OrderGiapHds
                .Include(o => o.UserAccount)
                .FirstOrDefaultAsync(o => o.OrderGiapHdid == id);
            return order ?? new OrderGiapHd();
        }

        public async Task<List<OrderGiapHd>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _context.OrderGiapHds
                .Include(o => o.UserAccount)
                .Where(o => o.UserAccountId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
            return orders ?? new List<OrderGiapHd>();
        }

        public async Task<List<OrderGiapHd>> GetOrdersByStatusAsync(string status)
        {
            var orders = await _context.OrderGiapHds
                .Include(o => o.UserAccount)
                .Where(o => o.Status == status)
                .ToListAsync();
            return orders ?? new List<OrderGiapHd>();
        }

        public async Task<List<OrderGiapHd>> GetActiveOrdersAsync()
        {
            var activeOrders = await _context.OrderGiapHds
                .Include(o => o.UserAccount)
                .Where(o => o.IsActive == true)
                .ToListAsync();
            return activeOrders ?? new List<OrderGiapHd>();
        }

        public async Task<PaginationResult<List<OrderGiapHd>>> SearchOrdersWithPagingAsync(string status, string paymentStatus, int? userId, int page, int pageSize)
        {
            var ordersQuery = _context.OrderGiapHds
                .Include(o => o.UserAccount)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                ordersQuery = ordersQuery.Where(o => o.Status.Contains(status));
            }

            if (!string.IsNullOrEmpty(paymentStatus))
            {
                ordersQuery = ordersQuery.Where(o => o.PaymentStatus.Contains(paymentStatus));
            }

            if (userId.HasValue)
            {
                ordersQuery = ordersQuery.Where(o => o.UserAccountId == userId.Value);
            }

            var orders = await ordersQuery.OrderByDescending(o => o.OrderDate).ToListAsync();
            var totalItems = orders.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            orders = orders.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var result = new PaginationResult<List<OrderGiapHd>>
            {
                TotalItems = totalItems,
                TotalPage = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = orders
            };
            return result;
        }

        public async Task<bool> UpdateStatusAsync(int id, string status, string? notes = null)
        {
            var order = await _context.OrderGiapHds.FindAsync(id);
            if (order == null) return false;

            order.Status = status;
            if (!string.IsNullOrEmpty(notes))
            {
                order.Notes = notes;
            }
            order.ModifiedBy = "System";

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatePaymentStatusAsync(int id, string paymentStatus)
        {
            var order = await _context.OrderGiapHds.FindAsync(id);
            if (order == null) return false;

            order.PaymentStatus = paymentStatus;
            order.ModifiedBy = "System";

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PaginationResult<List<OrderGiapHd>>> SearchAsync(string status, string paymentStatus, int? userId, int page, int pageSize)
        {
            var ordersQuery = _context.OrderGiapHds
                .Include(o => o.UserAccount)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                ordersQuery = ordersQuery.Where(o => o.Status.Contains(status));
            }

            if (!string.IsNullOrEmpty(paymentStatus))
            {
                ordersQuery = ordersQuery.Where(o => o.PaymentStatus.Contains(paymentStatus));
            }

            if (userId.HasValue)
            {
                ordersQuery = ordersQuery.Where(o => o.UserAccountId == userId.Value);
            }

            var orders = await ordersQuery.OrderByDescending(o => o.OrderDate).ToListAsync();
            var totalItems = orders.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            orders = orders.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var result = new PaginationResult<List<OrderGiapHd>>
            {
                TotalItems = totalItems,
                TotalPage = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = orders
            };
            return result;
        }

        public async Task<List<OrderGiapHd>> GetByUserIdAsync(int userId)
        {
            return await _context.OrderGiapHds
                .Include(o => o.UserAccount)
                .Where(o => o.UserAccountId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<List<OrderGiapHd>> GetByStatusAsync(string status)
        {
            return await _context.OrderGiapHds
                .Include(o => o.UserAccount)
                .Where(o => o.Status == status)
                .ToListAsync();
        }

        public async Task<List<OrderGiapHd>> GetByPaymentStatusAsync(string paymentStatus)
        {
            return await _context.OrderGiapHds
                .Include(o => o.UserAccount)
                .Where(o => o.PaymentStatus == paymentStatus)
                .ToListAsync();
        }

        public async Task<bool> CompleteOrderAsync(int id)
        {
            var order = await _context.OrderGiapHds.FindAsync(id);
            if (order == null) return false;

            order.Status = "Completed";
            order.CompletedDate = DateTime.Now;
            order.ModifiedBy = "System";

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelOrderAsync(int id, string? cancelReason = null)
        {
            var order = await _context.OrderGiapHds.FindAsync(id);
            if (order == null) return false;

            order.Status = "Cancelled";
            order.CancelledDate = DateTime.Now;
            if (!string.IsNullOrEmpty(cancelReason))
            {
                order.CancelReason = cancelReason;
            }
            order.ModifiedBy = "System";

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
