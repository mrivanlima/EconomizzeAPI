using Economizze.Library;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EconomizzeAPI.Model
{
    public class QuoteViewModel
    {

        public long QuoteId { get; set; }

        public int UserId { get; set; }

        public int NeighborhoodId { get; set; }

        [StringLength(256)]
        public string PrescriptionUrl { get; set; } = string.Empty;

        public bool IsExpired { get; set; } = false;

        [Required]
        public int CreatedBy { get; set; }


        public DateTime CreatedOn { get; set; }

        [Required]
        public int ModifiedBy { get; set; }


        public DateTime ModifiedOn { get; set; }

    }
}
