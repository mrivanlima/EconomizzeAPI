using System.ComponentModel.DataAnnotations;

namespace EconomizzeAPI.Model
{
    public class QuoteProductViewModel
    {
        public long QuoteId { get; set; }

        public int ProductId { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public int ModifiedBy { get; set; }
    
        public DateTime ModifiedOn { get; set; }
    }
}
