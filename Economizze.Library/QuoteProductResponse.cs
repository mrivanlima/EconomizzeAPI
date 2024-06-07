using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economizze.Library
{
    public class QuoteProductResponse
    {
        public long QuoteId { get; set; }
        public int ProductId { get; set; }
        public int DrugstoreId { get; set; }
        public int ProductVersionId { get; set; }
        public decimal RegularPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal FinalPrice { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
