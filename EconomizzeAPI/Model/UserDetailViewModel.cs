namespace EconomizzeAPI.Model
{
    public class UserDetailViewModel
    {
        // Fields from user_login table
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        public int PasswordAttempts { get; set; }
        public bool ChangedInitialPassword { get; set; }
        public DateTime? LockedTime { get; set; }

        // Fields from user table
        public int UserId { get; set; }
        public string UserFirstName { get; set; } = string.Empty;
        public string UserMiddleName { get; set; } = string.Empty;
        public string UserLastName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Rg { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public Guid UserUniqueId { get; set; }
    }
}
