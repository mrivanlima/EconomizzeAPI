using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economizze.Library
{
    public class QuoteProduct
    {
        public long QuoteId { get; set; }
        public int ProductId { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
