using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using DNATestingSystem.Repository.NhanVT.Models;
using DNATestingSystem.Repository.TienDM.ModelExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAppointmentsTienDm = DNATestingSystem.Repository.NhanVT.ModelExtensions.SearchAppointmentsTienDm;
using ApproveAppointmentDto = DNATestingSystem.Repository.TienDM.ModelExtensions.ApproveAppointmentDto;
using DenyAppointmentDto = DNATestingSystem.Repository.TienDM.ModelExtensions.DenyAppointmentDto;
using UpdateAppointmentStatusDto = DNATestingSystem.Repository.TienDM.ModelExtensions.UpdateAppointmentStatusDto;
using AppointmentTimelineDto = DNATestingSystem.Repository.TienDM.ModelExtensions.AppointmentTimelineDto;

namespace DNATestingSystem.Services.NhanVT
{
    public interface IAppointmentsTienDmService
    {
        Task<List<AppointmentsTienDm>> GetAllAsync();
        Task<PaginationResult<List<AppointmentsTienDm>>> GetAllPaginatedAsync(int page, int pageSize);
        Task<AppointmentsTienDm> GetByIdAsync(int id);
        Task<PaginationResult<List<AppointmentsTienDm>>> SearchAsync(int id, string contactPhone, decimal totalAmount, int page, int pageSize);
        Task<PaginationResult<List<AppointmentsTienDm>>> SearchAsync(SearchAppointmentsTienDm searchRequest);
        Task<int> CreateAsync(AppointmentsTienDm entity);
        Task<int> UpdateAsync(AppointmentsTienDm entity);
        Task<bool> DeleteAsync(int id);

        // DTO Methods
        Task<int> CreateFromDtoAsync(CreateAppointmentsTienDmDto createDto);
        Task<int> UpdateFromDtoAsync(UpdateAppointmentsTienDmDto updateDto);
        Task<AppointmentsTienDmDisplayDto?> GetDisplayDtoByIdAsync(int id);
        Task<PaginationResult<List<AppointmentsTienDmDisplayDto>>> GetDisplayDtosPaginatedAsync(SearchAppointmentsTienDm searchRequest);
        Task<List<AppointmentsTienDmDisplayDto>> GetDisplayDtosByUserIdAsync(int userId);

        // Approval/Denial Methods
        Task<bool> ApproveAppointmentAsync(ApproveAppointmentDto approveDto);
        Task<bool> DenyAppointmentAsync(DenyAppointmentDto denyDto);
        Task<bool> UpdateAppointmentStatusAsync(UpdateAppointmentStatusDto statusDto);
        Task<AppointmentTimelineDto?> GetAppointmentTimelineAsync(int appointmentId);
        Task<List<AppointmentsTienDmDisplayDto>> GetPendingAppointmentsForStaffAsync();

    }
}
