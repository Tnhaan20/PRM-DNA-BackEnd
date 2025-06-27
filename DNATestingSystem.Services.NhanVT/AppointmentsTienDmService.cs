using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using DNATestingSystem.Repository.NhanVT.Models;
using DNATestingSystem.Repository.NhanVT;
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
using TimelineStepDto = DNATestingSystem.Repository.TienDM.ModelExtensions.TimelineStepDto;

namespace DNATestingSystem.Services.NhanVT
{
    public class AppointmentsTienDmService : IAppointmentsTienDmService
    {
        private readonly AppointmentsTienDmRepository _repository;
        public AppointmentsTienDmService()
            => _repository = new AppointmentsTienDmRepository();

        public async Task<int> CreateAsync(AppointmentsTienDm appointmentsTien)
        {
            return await _repository.CreateAsync(appointmentsTien);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
        public async Task<List<AppointmentsTienDm>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<PaginationResult<List<AppointmentsTienDm>>> GetAllPaginatedAsync(int page, int pageSize)
        {
            // Use the new optimized repository method
            return await _repository.GetAllPaginatedAsync(page, pageSize) ?? new PaginationResult<List<AppointmentsTienDm>>();
        }

        public async Task<AppointmentsTienDm> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<PaginationResult<List<AppointmentsTienDm>>> SearchAsync(int id, string contactPhone, decimal totalAmount, int page, int pageSize)
        {
            var paginationResult = await _repository.SearchAsync(id, contactPhone, totalAmount, page, pageSize);
            return paginationResult ?? new PaginationResult<List<AppointmentsTienDm>>();
        }

        public async Task<PaginationResult<List<AppointmentsTienDm>>> SearchAsync(SearchAppointmentsTienDm searchRequest)
        {
            var paginationResult = await _repository.SearchAsync(searchRequest);
            return paginationResult ?? new PaginationResult<List<AppointmentsTienDm>>();
        }

        public async Task<int> UpdateAsync(AppointmentsTienDm appointmentsTien)
        {
            return await _repository.UpdateAsync(appointmentsTien);
        }

        // DTO Methods
        /// <summary>
        /// Create appointment using DTO to avoid navigation property validation issues
        /// </summary>
        public async Task<int> CreateFromDtoAsync(CreateAppointmentsTienDmDto createDto)
        {
            return await _repository.CreateFromDtoAsync(createDto);
        }

        /// <summary>
        /// Update appointment using DTO to avoid navigation property validation issues
        /// </summary>
        public async Task<int> UpdateFromDtoAsync(UpdateAppointmentsTienDmDto updateDto)
        {
            return await _repository.UpdateFromDtoAsync(updateDto);
        }

        /// <summary>
        /// Get appointment with related data for display
        /// </summary>
        public async Task<AppointmentsTienDmDisplayDto?> GetDisplayDtoByIdAsync(int id)
        {
            return await _repository.GetDisplayDtoByIdAsync(id);
        }

        /// <summary>
        /// Get paginated appointments as display DTOs
        /// </summary>
        public async Task<PaginationResult<List<AppointmentsTienDmDisplayDto>>> GetDisplayDtosPaginatedAsync(SearchAppointmentsTienDm searchRequest)
        {
            return await _repository.GetDisplayDtosPaginatedAsync(searchRequest);
        }

        /// <summary>
        /// Approve appointment by updating status to Confirmed
        /// </summary>
        public async Task<bool> ApproveAppointmentAsync(DNATestingSystem.Repository.TienDM.ModelExtensions.ApproveAppointmentDto approveDto)
        {
            var appointment = await _repository.GetByIdAsync(approveDto.AppointmentId);
            if (appointment?.AppointmentsTienDmid == 0 || appointment == null)
                return false;

            // Update status to Confirmed (2)
            appointment.AppointmentStatusesTienDmid = 2;
            appointment.ModifiedDate = approveDto.ApprovedDate;

            // Add approval notes if provided
            if (!string.IsNullOrEmpty(approveDto.ApprovalNotes))
            {
                appointment.Notes = string.IsNullOrEmpty(appointment.Notes)
                    ? $"Approved: {approveDto.ApprovalNotes}"
                    : $"{appointment.Notes}\nApproved: {approveDto.ApprovalNotes}";
            }

            var result = await _repository.UpdateAsync(appointment);
            return result > 0;
        }

        /// <summary>
        /// Deny appointment by updating status to Cancelled
        /// </summary>
        public async Task<bool> DenyAppointmentAsync(DNATestingSystem.Repository.TienDM.ModelExtensions.DenyAppointmentDto denyDto)
        {
            var appointment = await _repository.GetByIdAsync(denyDto.AppointmentId);
            if (appointment?.AppointmentsTienDmid == 0 || appointment == null)
                return false;

            // Update status to Cancelled (5)
            appointment.AppointmentStatusesTienDmid = 5;
            appointment.ModifiedDate = denyDto.DeniedDate;

            // Add denial reason
            appointment.Notes = string.IsNullOrEmpty(appointment.Notes)
                ? $"Denied: {denyDto.DenialReason}"
                : $"{appointment.Notes}\nDenied: {denyDto.DenialReason}";

            var result = await _repository.UpdateAsync(appointment);
            return result > 0;
        }

        /// <summary>
        /// Update appointment status
        /// </summary>
        public async Task<bool> UpdateAppointmentStatusAsync(DNATestingSystem.Repository.TienDM.ModelExtensions.UpdateAppointmentStatusDto statusDto)
        {
            var appointment = await _repository.GetByIdAsync(statusDto.AppointmentId);
            if (appointment?.AppointmentsTienDmid == 0 || appointment == null)
                return false;

            appointment.AppointmentStatusesTienDmid = statusDto.NewStatusId;
            appointment.ModifiedDate = DateTime.UtcNow;

            // Add status notes if provided
            if (!string.IsNullOrEmpty(statusDto.StatusNotes))
            {
                appointment.Notes = string.IsNullOrEmpty(appointment.Notes)
                    ? $"Status Update: {statusDto.StatusNotes}"
                    : $"{appointment.Notes}\nStatus Update: {statusDto.StatusNotes}";
            }

            var result = await _repository.UpdateAsync(appointment);
            return result > 0;
        }

        /// <summary>
        /// Get appointment timeline for customer tracking
        /// </summary>
        public async Task<DNATestingSystem.Repository.TienDM.ModelExtensions.AppointmentTimelineDto?> GetAppointmentTimelineAsync(int appointmentId)
        {
            var displayDto = await _repository.GetDisplayDtoByIdAsync(appointmentId);
            if (displayDto == null)
                return null;

            var timeline = new DNATestingSystem.Repository.TienDM.ModelExtensions.AppointmentTimelineDto
            {
                AppointmentId = displayDto.AppointmentsTienDmid,
                CustomerName = displayDto.UserName ?? "Unknown",
                ServiceName = displayDto.ServiceName ?? "Unknown Service",
                AppointmentDate = displayDto.AppointmentDate,
                AppointmentTime = displayDto.AppointmentTime,
                CurrentStatus = displayDto.StatusName ?? "Unknown",
                StatusId = displayDto.AppointmentStatusesTienDmid,
                TotalAmount = displayDto.TotalAmount,
                IsPaid = displayDto.IsPaid ?? false,
                SamplingMethod = displayDto.SamplingMethod,
                Address = displayDto.Address,
                ContactPhone = displayDto.ContactPhone,
                Notes = displayDto.Notes,
                Timeline = GenerateTimelineSteps(displayDto.AppointmentStatusesTienDmid, displayDto.CreatedDate, displayDto.ModifiedDate)
            };

            return timeline;
        }

        /// <summary>
        /// Get pending appointments for staff review
        /// </summary>
        public async Task<List<AppointmentsTienDmDisplayDto>> GetPendingAppointmentsForStaffAsync()
        {
            var searchRequest = new SearchAppointmentsTienDm
            {
                AppointmentStatusesTienDmid = 1, // Pending status
                CurrentPage = 1,
                PageSize = 100 // Get up to 100 pending appointments
            };

            var result = await _repository.GetDisplayDtosPaginatedAsync(searchRequest);
            return result?.Items ?? new List<AppointmentsTienDmDisplayDto>();
        }

        /// <summary>
        /// Generate timeline steps for appointment tracking
        /// </summary>
        private List<DNATestingSystem.Repository.TienDM.ModelExtensions.TimelineStepDto> GenerateTimelineSteps(int currentStatusId, DateTime? createdDate, DateTime? modifiedDate)
        {
            var steps = new List<DNATestingSystem.Repository.TienDM.ModelExtensions.TimelineStepDto>
            {
                new DNATestingSystem.Repository.TienDM.ModelExtensions.TimelineStepDto
                {
                    StepNumber = 1,
                    StepName = "Chờ xử lý",
                    Description = "Đơn đặt lịch đã được gửi và đang chờ xác nhận",
                    IsCompleted = currentStatusId >= 1,
                    IsActive = currentStatusId == 1,
                    CompletedDate = createdDate
                },
                new TimelineStepDto
                {
                    StepNumber = 2,
                    StepName = "Đã xác nhận",
                    Description = "Đơn đặt lịch đã được xác nhận bởi nhân viên",
                    IsCompleted = currentStatusId >= 2 && currentStatusId != 5,
                    IsActive = currentStatusId == 2,
                    CompletedDate = currentStatusId >= 2 && currentStatusId != 5 ? modifiedDate : null
                },
                new TimelineStepDto
                {
                    StepNumber = 3,
                    StepName = "Đang thực hiện",
                    Description = "Đang tiến hành thu thập mẫu và xét nghiệm",
                    IsCompleted = currentStatusId >= 3 && currentStatusId != 5,
                    IsActive = currentStatusId == 3,
                    CompletedDate = currentStatusId >= 3 && currentStatusId != 5 ? modifiedDate : null
                },
                new TimelineStepDto
                {
                    StepNumber = 4,
                    StepName = "Hoàn tất",
                    Description = "Xét nghiệm hoàn tất, kết quả đã sẵn sàng",
                    IsCompleted = currentStatusId == 4,
                    IsActive = currentStatusId == 4,
                    CompletedDate = currentStatusId == 4 ? modifiedDate : null
                }
            };

            // Add cancellation step if applicable
            if (currentStatusId == 5)
            {
                steps.Add(new TimelineStepDto
                {
                    StepNumber = 5,
                    StepName = "Đã hủy",
                    Description = "Đơn đặt lịch đã bị hủy",
                    IsCompleted = true,
                    IsActive = false,
                    CompletedDate = modifiedDate
                });
            }

            return steps;
        }
    }
}
