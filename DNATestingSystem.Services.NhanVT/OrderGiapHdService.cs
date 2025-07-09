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
    public class OrderGiapHdService : IOrderGiapHdService
    {
        private readonly OrderGiapHdRepository _repository;

        public OrderGiapHdService()
            => _repository = new OrderGiapHdRepository();

        public async Task<List<OrderGiapHd>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<OrderGiapHd> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<OrderGiapHd>> GetByUserIdAsync(int userId)
        {
            return await _repository.GetByUserIdAsync(userId);
        }

        public async Task<List<OrderGiapHd>> GetByStatusAsync(string status)
        {
            return await _repository.GetByStatusAsync(status);
        }

        public async Task<List<OrderGiapHd>> GetByPaymentStatusAsync(string paymentStatus)
        {
            return await _repository.GetByPaymentStatusAsync(paymentStatus);
        }

        public async Task<PaginationResult<List<OrderGiapHd>>> SearchAsync(string status, string paymentStatus, int? userId, int page, int pageSize)
        {
            return await _repository.SearchAsync(status, paymentStatus, userId, page, pageSize);
        }

        public async Task<int> CreateAsync(OrderGiapHd entity)
        {
            return await _repository.CreateAsync(entity);
        }

        public async Task<int> UpdateAsync(OrderGiapHd entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;
            return await _repository.RemoveAsync(entity);
        }

        public async Task<bool> UpdateStatusAsync(int id, string status)
        {
            return await _repository.UpdateStatusAsync(id, status);
        }

        public async Task<bool> UpdatePaymentStatusAsync(int id, string paymentStatus)
        {
            return await _repository.UpdatePaymentStatusAsync(id, paymentStatus);
        }

        public async Task<bool> CancelOrderAsync(int id, string reason)
        {
            return await _repository.CancelOrderAsync(id, reason);
        }

        public async Task<bool> CompleteOrderAsync(int id)
        {
            return await _repository.CompleteOrderAsync(id);
        }
    }
}
