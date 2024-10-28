using System.ComponentModel.DataAnnotations;

namespace EconomizzeAPI.Model
{
    public class DrugstoreViewModel
    {

        public int DrugstoreId { get; set; }

        [StringLength(200)]
        public string DrugstoreName { get; set; } = string.Empty;

        [StringLength(200)]
        public string DrugstoreNameAscii { get; set; } = string.Empty;

        public int AddressId { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
