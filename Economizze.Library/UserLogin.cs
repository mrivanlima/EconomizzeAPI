using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economizze.Library
{
    public class UserLogin
    {
        public int UserId { get; set; }
		public Guid UserUniqueId { get; set; }
		public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        public short PasswordAttempts { get; set; }
        public bool ChangedInitialPassword { get; set; }
        public DateTime? LockedTime { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
