using Economizze.Library;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EconomizzeAPI.Model
{
    public class NeighborhoodViewModel
    {
        public int NeighborhoodId { get; set; }

        [Required]
        [StringLength(100)]
        public string NeighborhoodName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string NeighborhoodNameAscii { get; set; } = string.Empty;

        public double? Longitude { get; set; }
        public double? Latitude { get; set; }

        public short CityId { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

    }
}
