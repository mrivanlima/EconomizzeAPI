using Economizze.Library;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EconomizzeAPI.Model
{
    public class QuoteResponseViewModel
    {

        public long QuoteId { get; set; }


        public int DrugstoreId { get; set; }


        public int ProductVersionId { get; set; }

        public decimal TotalPrice { get; set; }


        public decimal DiscountPercentage { get; set; }

        public decimal TotalFinalPrice { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public int CreatedBy { get; set; }


        public DateTime CreatedOn { get; set; }

        [Required]
        public int ModifiedBy { get; set; }


        public DateTime ModifiedOn { get; set; }

        
    }
}
