using System.ComponentModel.DataAnnotations;

namespace EconomizzeAPI.Model
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        [StringLength(200)]
        public string ProductName { get; set; } = string.Empty;

        [StringLength(200)]
        public string ProductNameAscii { get; set; } = string.Empty;

        [StringLength(20)]
        public string ProductConcentration { get; set; } = string.Empty;

        public short ProductQuantity { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
