using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Features.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public string Salt { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; } = UserRole.Staff;

        public UserStatus Status { get; set; } = UserStatus.Active;

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        public string? ProfileImageUrl { get; set; }

        // Security tracking
        public int FailedLoginAttempts { get; set; } = 0;
        public DateTime? LockedUntil { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public string? LastLoginIp { get; set; }

        // Password reset
        public string? PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpiry { get; set; }

        // Audit fields
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false; // Soft delete

        // Computed helpers
        public bool IsLocked => LockedUntil.HasValue && LockedUntil > DateTime.UtcNow;
        public bool IsActive => Status == UserStatus.Active && !IsDeleted;
    }

    public enum UserRole
    {
        Admin = 0,
        Manager = 1,
        Staff = 2,
        Viewer = 3
    }

    public enum UserStatus
    {
        Active = 0,
        Inactive = 1,
        Suspended = 2,
        PendingVerification = 3
    }
}
