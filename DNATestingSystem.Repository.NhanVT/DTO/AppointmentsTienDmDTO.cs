using System;
using System.ComponentModel.DataAnnotations;

namespace DNATestingSystem.Repository.TienDM.ModelExtensions
{
    /// <summary>
    /// DTO for creating new appointments - only includes necessary fields for input
    /// </summary>
    public class CreateAppointmentsTienDmDto
    {
        [Required(ErrorMessage = "User Account is required")]
        public int UserAccountId { get; set; }

        [Required(ErrorMessage = "Service selection is required")]
        public int ServicesNhanVtid { get; set; }

        [Required(ErrorMessage = "Appointment status is required")]
        public int AppointmentStatusesTienDmid { get; set; }

        [Required(ErrorMessage = "Appointment date is required")]
        public DateOnly AppointmentDate { get; set; }

        [Required(ErrorMessage = "Appointment time is required")]
        public TimeOnly AppointmentTime { get; set; }

        [Required(ErrorMessage = "Sampling method is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Sampling method must be between 3 and 100 characters")]
        public string SamplingMethod { get; set; } = null!;

        [StringLength(250, ErrorMessage = "Address cannot exceed 250 characters")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Contact phone is required")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Contact phone must be between 10 and 15 characters")]
        public string ContactPhone { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string? Notes { get; set; }

        [Required(ErrorMessage = "Total amount is required")]
        public decimal TotalAmount { get; set; }

        public bool? IsPaid { get; set; } = false;
    }

    /// <summary>
    /// DTO for updating appointments - includes ID and fields that can be updated
    /// </summary>
    public class UpdateAppointmentsTienDmDto
    {
        [Required(ErrorMessage = "Appointment ID is required")]
        public int AppointmentsTienDmid { get; set; }

        [Required(ErrorMessage = "User Account is required")]
        public int UserAccountId { get; set; }

        [Required(ErrorMessage = "Service selection is required")]
        public int ServicesNhanVtid { get; set; }

        [Required(ErrorMessage = "Appointment status is required")]
        public int AppointmentStatusesTienDmid { get; set; }

        [Required(ErrorMessage = "Appointment date is required")]
        public DateOnly AppointmentDate { get; set; }

        [Required(ErrorMessage = "Appointment time is required")]
        public TimeOnly AppointmentTime { get; set; }

        [Required(ErrorMessage = "Sampling method is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Sampling method must be between 3 and 100 characters")]
        public string SamplingMethod { get; set; } = null!;

        [StringLength(250, ErrorMessage = "Address cannot exceed 250 characters")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Contact phone is required")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Contact phone must be between 10 and 15 characters")]
        public string ContactPhone { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string? Notes { get; set; }

        [Required(ErrorMessage = "Total amount is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Total amount must be between 0.01 and 999,999.99")]
        public decimal TotalAmount { get; set; }

        public bool? IsPaid { get; set; }
    }

    /// <summary>
    /// DTO for displaying appointment details - includes navigation properties for display
    /// </summary>
    public class AppointmentsTienDmDisplayDto
    {
        public int AppointmentsTienDmid { get; set; }
        public int UserAccountId { get; set; } // FK: Join to SystemUserAccount to get UserName
        public int ServicesNhanVtid { get; set; } // FK: Join to ServicesNhanVt to get ServiceName
        public int AppointmentStatusesTienDmid { get; set; } // FK: Join to AppointmentStatusesTienDm to get StatusName
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }
        public string SamplingMethod { get; set; } = null!;
        public string? Address { get; set; }
        public string ContactPhone { get; set; } = null!;
        public string? Notes { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public decimal TotalAmount { get; set; }
        public bool? IsPaid { get; set; }

        // Display fields populated via JOINs for UI
        public string? StatusName { get; set; } // From AppointmentStatusesTienDm
        public string? ServiceName { get; set; } // From ServicesNhanVt
        public string? UserName { get; set; } // From SystemUserAccount
        public string? UserEmail { get; set; } // From SystemUserAccount
    }

    /// <summary>
    /// DTO for approving appointments by staff
    /// </summary>
    public class ApproveAppointmentDto
    {
        [Required(ErrorMessage = "Appointment ID is required")]
        public int AppointmentId { get; set; }

        [StringLength(500, ErrorMessage = "Approval notes cannot exceed 500 characters")]
        public string? ApprovalNotes { get; set; }

        public int ApprovedBy { get; set; }

        public DateTime ApprovedDate { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// DTO for denying/cancelling appointments by staff
    /// </summary>
    public class DenyAppointmentDto
    {
        [Required(ErrorMessage = "Appointment ID is required")]
        public int AppointmentId { get; set; }

        [Required(ErrorMessage = "Reason for denial is required")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Denial reason must be between 10 and 500 characters")]
        public string DenialReason { get; set; } = null!;

        public int DeniedBy { get; set; }

        public DateTime DeniedDate { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// DTO for updating appointment status
    /// </summary>
    public class UpdateAppointmentStatusDto
    {
        [Required(ErrorMessage = "Appointment ID is required")]
        public int AppointmentId { get; set; }

        [Required(ErrorMessage = "New status is required")]
        [Range(1, 5, ErrorMessage = "Status must be between 1 and 5")]
        public int NewStatusId { get; set; }

        [StringLength(500, ErrorMessage = "Status notes cannot exceed 500 characters")]
        public string? StatusNotes { get; set; }

        public int UpdatedBy { get; set; }
    }

    /// <summary>
    /// DTO for appointment timeline display
    /// </summary>
    public class AppointmentTimelineDto
    {
        public int AppointmentId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string ServiceName { get; set; } = null!;
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }
        public string CurrentStatus { get; set; } = null!;
        public int StatusId { get; set; }
        public List<TimelineStepDto> Timeline { get; set; } = new List<TimelineStepDto>();
        public decimal TotalAmount { get; set; }
        public bool IsPaid { get; set; }
        public string SamplingMethod { get; set; } = null!;
        public string? Address { get; set; }
        public string ContactPhone { get; set; } = null!;
        public string? Notes { get; set; }
    }

    /// <summary>
    /// DTO for timeline step
    /// </summary>
    public class TimelineStepDto
    {
        public int StepNumber { get; set; }
        public string StepName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsCompleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string? Notes { get; set; }
    }
}
