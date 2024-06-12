using Economizze.Library;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EconomizzeAPI.Model
{
    public class StreetViewModel
    {

        public int StreetId { get; set; }

        [Required]
        [StringLength(150)]
        public string StreetName { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string StreetNameAscii { get; set; } = string.Empty;

        [StringLength(8)]
        public string ZipCode { get; set; } = string.Empty;

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }

        public int NeighborhoodId { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

    }
}
