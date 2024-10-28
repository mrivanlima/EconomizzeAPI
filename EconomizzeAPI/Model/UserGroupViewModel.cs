using System.ComponentModel.DataAnnotations;

namespace EconomizzeAPI.Model
{
    public class UserGroupViewModel
    {
        public short GroupId { get; set; }

        public int UserId { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? GroupStartDate { get; set; }

        public DateTime? GroupEndDate { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
