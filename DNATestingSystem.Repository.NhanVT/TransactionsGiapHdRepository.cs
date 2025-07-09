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
    public class TransactionsGiapHdRepository : GenericRepository<TransactionsGiapHd>
    {
        public TransactionsGiapHdRepository() { }

        public TransactionsGiapHdRepository(SE18_PRN232_SE1730_G3_DNATestingSystemContext context) => _context = context;

        public async Task<List<TransactionsGiapHd>> GetAllTransactionsAsync()
        {
            var allTransactions = await _context.TransactionsGiapHds
                .Include(t => t.OrderGiapHd)
                .ToListAsync();
            return allTransactions ?? new List<TransactionsGiapHd>();
        }

        public async Task<TransactionsGiapHd> GetTransactionByIdAsync(int id)
        {
            var transaction = await _context.TransactionsGiapHds
                .Include(t => t.OrderGiapHd)
                .FirstOrDefaultAsync(t => t.TransactionsGiapHdid == id);
            return transaction ?? new TransactionsGiapHd();
        }

        public async Task<List<TransactionsGiapHd>> GetTransactionsByOrderIdAsync(int orderId)
        {
            var transactions = await _context.TransactionsGiapHds
                .Include(t => t.OrderGiapHd)
                .Where(t => t.OrderGiapHdid == orderId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
            return transactions ?? new List<TransactionsGiapHd>();
        }

        public async Task<List<TransactionsGiapHd>> GetTransactionsByPaymentMethodAsync(string paymentMethod)
        {
            var transactions = await _context.TransactionsGiapHds
                .Include(t => t.OrderGiapHd)
                .Where(t => t.PaymentMethod == paymentMethod)
                .ToListAsync();
            return transactions ?? new List<TransactionsGiapHd>();
        }

        public async Task<List<TransactionsGiapHd>> GetRefundTransactionsAsync()
        {
            var refundTransactions = await _context.TransactionsGiapHds
                .Include(t => t.OrderGiapHd)
                .Where(t => t.IsRefund == true)
                .ToListAsync();
            return refundTransactions ?? new List<TransactionsGiapHd>();
        }

        public async Task<PaginationResult<List<TransactionsGiapHd>>> SearchTransactionsWithPagingAsync(string paymentMethod, string paymentGateway, bool? isRefund, int page, int pageSize)
        {
            var transactionsQuery = _context.TransactionsGiapHds
                .Include(t => t.OrderGiapHd)
                .AsQueryable();

            if (!string.IsNullOrEmpty(paymentMethod))
            {
                transactionsQuery = transactionsQuery.Where(t => t.PaymentMethod != null && t.PaymentMethod.Contains(paymentMethod));
            }

            if (!string.IsNullOrEmpty(paymentGateway))
            {
                transactionsQuery = transactionsQuery.Where(t => t.PaymentGateway != null && t.PaymentGateway.Contains(paymentGateway));
            }

            if (isRefund.HasValue)
            {
                transactionsQuery = transactionsQuery.Where(t => t.IsRefund == isRefund.Value);
            }

            var transactions = await transactionsQuery.OrderByDescending(t => t.CreatedAt).ToListAsync();
            var totalItems = transactions.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            transactions = transactions.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var result = new PaginationResult<List<TransactionsGiapHd>>
            {
                TotalItems = totalItems,
                TotalPage = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = transactions
            };
            return result;
        }

        public async Task<PaginationResult<List<TransactionsGiapHd>>> SearchAsync(int? orderId, string paymentMethod, bool? isRefund, int page, int pageSize)
        {
            var query = _context.TransactionsGiapHds
                .Include(t => t.OrderGiapHd)
                .AsQueryable();
                
            if (orderId.HasValue)
            {
                query = query.Where(t => t.OrderGiapHdid == orderId.Value);
            }
            
            if (!string.IsNullOrEmpty(paymentMethod))
            {
                query = query.Where(t => t.PaymentMethod != null && t.PaymentMethod.Contains(paymentMethod));
            }
            
            if (isRefund.HasValue)
            {
                query = query.Where(t => t.IsRefund == isRefund.Value);
            }
            
            var transactions = await query.OrderByDescending(t => t.CreatedAt).ToListAsync();
            var totalItems = transactions.Count;
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            
            transactions = transactions.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            
            return new PaginationResult<List<TransactionsGiapHd>>
            {
                TotalItems = totalItems,
                TotalPage = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = transactions
            };
        }
        
        public async Task<List<TransactionsGiapHd>> GetByOrderIdAsync(int orderId)
        {
            return await _context.TransactionsGiapHds
                .Include(t => t.OrderGiapHd)
                .Where(t => t.OrderGiapHdid == orderId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }
        
        public async Task<List<TransactionsGiapHd>> GetByPaymentMethodAsync(string paymentMethod)
        {
            return await _context.TransactionsGiapHds
                .Include(t => t.OrderGiapHd)
                .Where(t => t.PaymentMethod == paymentMethod)
                .ToListAsync();
        }
        
        public async Task<int> CreateRefundAsync(int originalTransactionId, string refundReason, decimal refundAmount)
        {
            var originalTransaction = await _context.TransactionsGiapHds.FindAsync(originalTransactionId);
            if (originalTransaction == null) return 0;
            
            var refundTransaction = new TransactionsGiapHd
            {
                OrderGiapHdid = originalTransaction.OrderGiapHdid,
                Amount = (long)refundAmount,
                PaymentMethod = originalTransaction.PaymentMethod,
                PaymentGateway = originalTransaction.PaymentGateway,
                TransactionReference = $"REFUND-{Guid.NewGuid().ToString().Substring(0, 8)}",
                IsRefund = true,
                RefundReason = refundReason,
                CreatedAt = DateTime.Now
            };
            
            _context.TransactionsGiapHds.Add(refundTransaction);
            await _context.SaveChangesAsync();
            return refundTransaction.TransactionsGiapHdid;
        }

        public async Task<List<TransactionsGiapHd>> GetByStatusAsync(string status)
        {
            var transactions = await _context.TransactionsGiapHds
                .Include(t => t.OrderGiapHd)
                .Where(t => t.OrderGiapHd != null && t.OrderGiapHd.Status.Contains(status))
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
            return transactions ?? new List<TransactionsGiapHd>();
        }
    }
}
