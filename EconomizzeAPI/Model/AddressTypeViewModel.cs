using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EconomizzeAPI.Model
{
    public class AddressTypeViewModel
    {

        public short AddressTypeId { get; set; }

        [Required]
        [StringLength(20)] // Assuming a max length of 100 for the name
        public string AddressTypeName { get; set; } = string.Empty;

        [Required]
        [StringLength(20)] // Assuming a max length of 100 for the ASCII name
        public string AddressTypeNameAscii { get; set; } = string.Empty;

        [Required]
        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
