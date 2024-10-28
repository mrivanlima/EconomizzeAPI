using System.ComponentModel.DataAnnotations;

namespace EconomizzeAPI.Model
{
    public class ProfessionalViewModel
    {
        public short ProfessionalId { get; set; }

        public short ProfessionId { get; set; }

        [StringLength(20)]
        public string ProfessionalName { get; set; } = string.Empty;

        [StringLength(20)]
        public string ProfessionalNameAscii { get; set; } = string.Empty;

        [StringLength(10)]
        public string CounselNumber { get; set; } = string.Empty;

        [Required]
        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
