using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EconomizzeAPI.Model
{
    public class GroupViewModel
    {

        public short GroupId { get; set; }

        [StringLength(50)]
        public string GroupName { get; set; } = string.Empty;

        [StringLength(50)]
        public string GroupNameAscii { get; set; } = string.Empty;

        [Required]
        public int CreatedBy { get; set; }


        public DateTime CreatedOn { get; set; }

        [Required]
        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
