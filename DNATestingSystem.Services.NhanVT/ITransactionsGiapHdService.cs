using DNATestingSystem.Repository.NhanVT.Models;
using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATestingSystem.Services.NhanVT
{
    public interface ITransactionsGiapHdService
    {
        Task<List<TransactionsGiapHd>> GetAllAsync();
        Task<TransactionsGiapHd> GetByIdAsync(int id);
        Task<List<TransactionsGiapHd>> GetByOrderIdAsync(int orderId);
        Task<List<TransactionsGiapHd>> GetByPaymentMethodAsync(string paymentMethod);
        Task<List<TransactionsGiapHd>> GetByStatusAsync(string status);
        Task<List<TransactionsGiapHd>> GetRefundTransactionsAsync();
        Task<PaginationResult<List<TransactionsGiapHd>>> SearchAsync(int? orderId, string paymentMethod, bool? isRefund, int page, int pageSize);
        Task<int> CreateAsync(TransactionsGiapHd entity);
        Task<int> UpdateAsync(TransactionsGiapHd entity);
        Task<bool> DeleteAsync(int id);
        Task<int> CreateRefundAsync(int originalTransactionId, string refundReason, decimal refundAmount);
    }
}
