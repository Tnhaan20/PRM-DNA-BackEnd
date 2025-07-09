using DNATestingSystem.Repository.NhanVT.Models;
using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATestingSystem.Services.NhanVT
{
    public interface IOrderGiapHdService
    {
        Task<List<OrderGiapHd>> GetAllAsync();
        Task<OrderGiapHd> GetByIdAsync(int id);
        Task<List<OrderGiapHd>> GetByUserIdAsync(int userId);
        Task<List<OrderGiapHd>> GetByStatusAsync(string status);
        Task<List<OrderGiapHd>> GetByPaymentStatusAsync(string paymentStatus);
        Task<PaginationResult<List<OrderGiapHd>>> SearchAsync(string status, string paymentStatus, int? userId, int page, int pageSize);
        Task<int> CreateAsync(OrderGiapHd entity);
        Task<int> UpdateAsync(OrderGiapHd entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateStatusAsync(int id, string status);
        Task<bool> UpdatePaymentStatusAsync(int id, string paymentStatus);
        Task<bool> CancelOrderAsync(int id, string reason);
        Task<bool> CompleteOrderAsync(int id);
    }
}
