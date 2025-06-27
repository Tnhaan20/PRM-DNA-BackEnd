using System.ComponentModel;

namespace DNATestingSystem.Repository.NhanVT.Enums
{
    /// <summary>
    /// Enum representing appointment status in the DNA Testing System
    /// </summary>
    public enum AppointmentStatus
    {
        /// <summary>
        /// Chờ xử lý - Pending approval
        /// </summary>
        [Description("Chờ xử lý")]
        Pending = 1,

        /// <summary>
        /// Đã xác nhận - Confirmed by staff
        /// </summary>
        [Description("Đã xác nhận")]
        Confirmed = 2,

        /// <summary>
        /// Đang thực hiện - In progress
        /// </summary>
        [Description("Đang thực hiện")]
        InProgress = 3,

        /// <summary>
        /// Hoàn tất - Completed
        /// </summary>
        [Description("Hoàn tất")]
        Completed = 4,

        /// <summary>
        /// Đã hủy - Cancelled
        /// </summary>
        [Description("Đã hủy")]
        Cancelled = 5
    }

    /// <summary>
    /// Extension methods for AppointmentStatus enum
    /// </summary>
    public static class AppointmentStatusExtensions
    {
        /// <summary>
        /// Get Vietnamese description of the appointment status
        /// </summary>
        /// <param name="status">Appointment status</param>
        /// <returns>Vietnamese description</returns>
        public static string GetDescription(this AppointmentStatus status)
        {
            var field = status.GetType().GetField(status.ToString());
            if (field != null)
            {
                var attribute = (DescriptionAttribute?)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                return attribute?.Description ?? status.ToString();
            }
            return status.ToString();
        }

        /// <summary>
        /// Get English description of the appointment status
        /// </summary>
        /// <param name="status">Appointment status</param>
        /// <returns>English description</returns>
        public static string GetEnglishDescription(this AppointmentStatus status)
        {
            return status switch
            {
                AppointmentStatus.Pending => "Pending",
                AppointmentStatus.Confirmed => "Confirmed",
                AppointmentStatus.InProgress => "In Progress",
                AppointmentStatus.Completed => "Completed",
                AppointmentStatus.Cancelled => "Cancelled",
                _ => status.ToString()
            };
        }

        /// <summary>
        /// Check if appointment can be cancelled
        /// </summary>
        /// <param name="status">Current status</param>
        /// <returns>True if can be cancelled</returns>
        public static bool CanBeCancelled(this AppointmentStatus status)
        {
            return status == AppointmentStatus.Pending || status == AppointmentStatus.Confirmed;
        }

        /// <summary>
        /// Check if appointment can be modified
        /// </summary>
        /// <param name="status">Current status</param>
        /// <returns>True if can be modified</returns>
        public static bool CanBeModified(this AppointmentStatus status)
        {
            return status == AppointmentStatus.Pending;
        }

        /// <summary>
        /// Check if appointment is in active state
        /// </summary>
        /// <param name="status">Current status</param>
        /// <returns>True if active</returns>
        public static bool IsActive(this AppointmentStatus status)
        {
            return status != AppointmentStatus.Cancelled && status != AppointmentStatus.Completed;
        }

        /// <summary>
        /// Get color code for UI display
        /// </summary>
        /// <param name="status">Appointment status</param>
        /// <returns>Color hex code</returns>
        public static string GetColorCode(this AppointmentStatus status)
        {
            return status switch
            {
                AppointmentStatus.Pending => "#FFC107", // Yellow
                AppointmentStatus.Confirmed => "#91C8E4", // Light Blue
                AppointmentStatus.InProgress => "#749BC2", // Medium Blue
                AppointmentStatus.Completed => "#28A745", // Green
                AppointmentStatus.Cancelled => "#DC3545", // Red
                _ => "#6C757D" // Gray
            };
        }

        /// <summary>
        /// Get progress percentage for timeline display
        /// </summary>
        /// <param name="status">Appointment status</param>
        /// <returns>Progress percentage (0-100)</returns>
        public static int GetProgressPercentage(this AppointmentStatus status)
        {
            return status switch
            {
                AppointmentStatus.Pending => 25,
                AppointmentStatus.Confirmed => 50,
                AppointmentStatus.InProgress => 75,
                AppointmentStatus.Completed => 100,
                AppointmentStatus.Cancelled => 0,
                _ => 0
            };
        }
    }
}
