using System.ComponentModel.DataAnnotations;

namespace EconomizzeAPI.Model
{
    public class CityViewModel
    {

        public short CityId { get; set; }

        [Required]
        [StringLength(50)]
        public string CityName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string CityNameAscii { get; set; } = string.Empty;

        public double? Longitude { get; set; }
        public double? Latitude { get; set; }

        public short StateId { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;

    }
}
