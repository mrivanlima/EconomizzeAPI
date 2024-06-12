using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EconomizzeAPI.Model
{
    public class ContactTypeViewModel
    {
        public short ContactTypeId { get; set; }

        [StringLength(20)]
        public string ContactTypeName { get; set; } = string.Empty;

        [StringLength(20)]
        public string ContactTypeNameAscii { get; set; } = string.Empty;

        [Required]
        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
    }
}
