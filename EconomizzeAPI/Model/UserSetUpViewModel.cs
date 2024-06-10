using System.ComponentModel.DataAnnotations;

namespace EconomizzeAPI.Model
{
    public class UserSetUpViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(100, ErrorMessage = "First name must be between 1 and 100 characters", MinimumLength = 1)]
        public string UserFirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(250, ErrorMessage = "Email must be between 1 and 250 characters", MinimumLength = 1)]
        public string UserEmail { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Username must be between 1 and 100 characters", MinimumLength = 1)]
        public string Username { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(200, ErrorMessage = "Email must be between 1 and 200 characters", MinimumLength = 1)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password hash is required")]
        [StringLength(100, ErrorMessage = "Password hash must be between 1 and 100 characters", MinimumLength = 1)]
        //[RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[!@#$%^&*()-_=+[\]{}|\\;:'"",.<>/?]).+$", ErrorMessage = "Password hash must contain at least one letter, one number, and one special character")]
        public string PasswordHash { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password salt is required")]
        [StringLength(100, ErrorMessage = "Password salt must be between 1 and 100 characters", MinimumLength = 1)]
        public string PasswordSalt { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Middle name must be between 1 and 100 characters", MinimumLength = 1)]
        public string UserMiddleName { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Last name must be between 1 and 100 characters", MinimumLength = 1)]
        public string UserLastName { get; set; } = string.Empty;

        [StringLength(11, ErrorMessage = "CPF must be 11 characters")]
        public string CPF { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "RG must be between 1 and 100 characters", MinimumLength = 1)]
        public string RG { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        public bool IsVerified { get; set; }

        public bool IsActive { get; set; }

        public bool IsLocked { get; set; }

        [Range(0, short.MaxValue, ErrorMessage = "Password attempts must be a non-negative number")]
        public short PasswordAttempts { get; set; }

        public bool ChangedInitialPassword { get; set; }

        public DateTime? LockedTime { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Created by must be a positive number")]
        public int? CreatedBy { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Modified by must be a positive number")]
        public int? ModifiedBy { get; set; }

        public Guid UserUniqueId { get; set; }
    }
}
