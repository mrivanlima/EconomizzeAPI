using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EconomizzeAPI.Model
{
    public class ProductVersionViewControl
    {

        public short ProductVersionId { get; set; }

        [StringLength(20)]
        public string ProductVersionName { get; set; } = string.Empty;

        [StringLength(20)]
        public string ProductVersionAscii { get; set; } = string.Empty;

        [Required]
        public int CreatedBy { get; set; }

 
        public DateTime CreatedOn { get; set; }

        [Required]
        public int ModifiedBy { get; set; }


        public DateTime ModifiedOn { get; set; }
    }
}
