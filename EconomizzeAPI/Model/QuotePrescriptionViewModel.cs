using System.ComponentModel.DataAnnotations;

namespace EconomizzeHybrid.Model
{
	public class QuotePrescriptionViewModel
	{
		public long QuoteId { get; set; }

		public long PrescriptionId { get; set; }

		[Required]
		public int CreatedBy { get; set; }

		public DateTime CreatedOn { get; set; }

		[Required]
		public int ModifiedBy { get; set; }

		public DateTime ModifiedOn { get; set; }
	}
}
