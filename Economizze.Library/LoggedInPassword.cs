using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economizze.Library
{
    public class LoggedInPassword
    {
        public int UserId { get; set; }
        public Guid UserUniqueId { get; set; }
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string CurrentPassword { get; set; } = string.Empty;
        public int ModifiedBy { get; set; }
    }
}
