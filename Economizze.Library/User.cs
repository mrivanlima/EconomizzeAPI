using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economizze.Library
{
    public class User
    {
        public int UserId { get; set; }
        public string UserFirstName { get; set; } = string.Empty;
        public string UserMiddleName { get; set; } = string.Empty;
        public string UserLastName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Rg { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
