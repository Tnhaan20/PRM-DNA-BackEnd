using System.ComponentModel;

namespace DNATestingSystem.Repository.NhanVT.Enums
{
    /// <summary>
    /// Enum representing user roles in the DNA Testing System
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// Guest user - No authorization required
        /// </summary>
        [Description("Guest - No authorization")]
        Guest = 0,

        /// <summary>
        /// Customer - Can book appointments and view results
        /// </summary>
        [Description("Customer")]
        Customer = 1,

        /// <summary>
        /// Staff - Can manage appointments and samples
        /// </summary>
        [Description("Staff")]
        Staff = 2,

        /// <summary>
        /// Manager - Can manage staff and view reports
        /// </summary>
        [Description("Manager")]
        Manager = 3,

        /// <summary>
        /// Admin - Full system access
        /// </summary>
        [Description("Admin")]
        Admin = 4
    }

    /// <summary>
    /// Extension methods for UserRole enum
    /// </summary>
    public static class UserRoleExtensions
    {
        /// <summary>
        /// Get description of the user role
        /// </summary>
        /// <param name="role">User role</param>
        /// <returns>Description string</returns>
        public static string GetDescription(this UserRole role)
        {
            var field = role.GetType().GetField(role.ToString());
            if (field != null)
            {
                var attribute = (DescriptionAttribute?)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                return attribute?.Description ?? role.ToString();
            }
            return role.ToString();
        }

        /// <summary>
        /// Check if user has authorization for specific operations
        /// </summary>
        /// <param name="role">User role</param>
        /// <returns>True if authorized</returns>
        public static bool RequiresAuthorization(this UserRole role)
        {
            return role != UserRole.Guest;
        }

        /// <summary>
        /// Check if user can manage appointments
        /// </summary>
        /// <param name="role">User role</param>
        /// <returns>True if can manage</returns>
        public static bool CanManageAppointments(this UserRole role)
        {
            return role >= UserRole.Staff;
        }

        /// <summary>
        /// Check if user can access admin features
        /// </summary>
        /// <param name="role">User role</param>
        /// <returns>True if can access</returns>
        public static bool CanAccessAdmin(this UserRole role)
        {
            return role >= UserRole.Manager;
        }
    }
}
