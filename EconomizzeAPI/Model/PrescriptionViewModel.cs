using System.ComponentModel.DataAnnotations;

namespace EconomizzeHybrid.Model
{
	public class PrescriptionViewModel
	{
		public long PrescriptionId { get; set; }

		public Guid PrescriptionUnique { get; set; }

		[StringLength(200)]
		public String PrescriptionUrl { get; set; } = String.Empty;

		[Required]
		public int CreatedBy { get; set; }

		public DateTime CreatedOn { get; set; }

		[Required]
		public int ModifiedBy { get; set; }

		public DateTime ModifiedOn { get; set; }

		public int FacilityId { get; set; }

		public int ProfessionalId { get; set; }
	}
}
