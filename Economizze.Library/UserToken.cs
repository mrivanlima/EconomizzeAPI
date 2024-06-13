using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economizze.Library
{
    public class UserToken
    {
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;

        public DateTime? TokenStartDate { get; set; }

        public DateTime? TokenExpirationDate { get; set; }

        public bool IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
