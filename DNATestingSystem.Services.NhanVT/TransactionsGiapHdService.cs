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
    public class TransactionsGiapHdService : ITransactionsGiapHdService
    {
        private readonly TransactionsGiapHdRepository _repository;

        public TransactionsGiapHdService()
            => _repository = new TransactionsGiapHdRepository();

        public async Task<List<TransactionsGiapHd>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TransactionsGiapHd> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<TransactionsGiapHd>> GetByOrderIdAsync(int orderId)
        {
            return await _repository.GetByOrderIdAsync(orderId);
        }

        public async Task<List<TransactionsGiapHd>> GetByPaymentMethodAsync(string paymentMethod)
        {
            return await _repository.GetByPaymentMethodAsync(paymentMethod);
        }
        
        public async Task<List<TransactionsGiapHd>> GetByStatusAsync(string status)
        {
            return await _repository.GetByStatusAsync(status);
        }

        public async Task<List<TransactionsGiapHd>> GetRefundTransactionsAsync()
        {
            return await _repository.GetRefundTransactionsAsync();
        }

        public async Task<PaginationResult<List<TransactionsGiapHd>>> SearchAsync(int? orderId, string paymentMethod, bool? isRefund, int page, int pageSize)
        {
            return await _repository.SearchAsync(orderId, paymentMethod, isRefund, page, pageSize);
        }

        public async Task<int> CreateAsync(TransactionsGiapHd entity)
        {
            return await _repository.CreateAsync(entity);
        }

        public async Task<int> UpdateAsync(TransactionsGiapHd entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;
            return await _repository.RemoveAsync(entity);
        }

        public async Task<int> CreateRefundAsync(int originalTransactionId, string refundReason, decimal refundAmount)
        {
            return await _repository.CreateRefundAsync(originalTransactionId, refundReason, refundAmount);
        }
    }
}
