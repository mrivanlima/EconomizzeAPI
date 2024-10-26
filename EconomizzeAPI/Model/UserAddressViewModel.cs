using System.ComponentModel.DataAnnotations;

namespace EconomizzeAPI.Model
{
    public class UserAddressViewModel
    {
        public int UserId { get; set; }
        public int AddressId { get; set; }

        public int StreetId { get; set; }

        public string Complement { get; set; } = string.Empty;

        public bool MainAddress { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
