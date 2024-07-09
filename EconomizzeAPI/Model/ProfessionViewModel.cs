using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EconomizzeAPI.Model
{
    public class ProfessionViewModel
    {

        public short ProfessionId { get; set; }

        [StringLength(20)]
        public string ProfessionName { get; set; } = string.Empty;

        [StringLength(20)]
        public string ProfessionNameAscii { get; set; } = string.Empty;

        [Required]
        public int CreatedBy { get; set; }


        public DateTime CreatedOn { get; set; }

        [Required]
        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
