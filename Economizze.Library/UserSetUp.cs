using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economizze.Library
{
    public class UserSetUp
    {
        public int UserId { get; set; }
        public string UserFirstName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;
        public string UserMiddleName { get; set; } = string.Empty;
        public string UserLastName { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string RG { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        public short PasswordAttempts { get; set; }
        public bool ChangedInitialPassword { get; set; }
        public DateTime? LockedTime { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }

    }
}
