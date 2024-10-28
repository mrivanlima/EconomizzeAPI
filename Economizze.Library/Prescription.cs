namespace Economizze.Library
{
	public class Prescription
	{
		public long PrescriptionId { get; set; }
		public Guid PrescriptionUnique { get; set; }
		public String PrescriptionUrl { get; set; } = String.Empty;
		public int CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public int ModifiedBy { get; set; }
		public DateTime ModifiedOn { get; set; }
		public int FacilityId { get; set; }
		public int ProfessionalId { get; set; }
	}
}
