using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EconomizzeAPI.Model
{
    public class RoleViewModel
    {

        public short RoleId { get; set; }

        [StringLength(50)]
        public string RoleName { get; set; } = string.Empty;

        [StringLength(50)]
        public string RoleNameAscii { get; set; } = string.Empty;

        [Required]
        public int CreatedBy { get; set; }


        public DateTime CreatedOn { get; set; }

        [Required]
        public int ModifiedBy { get; set; }


        public DateTime ModifiedOn { get; set; }
    }
}
