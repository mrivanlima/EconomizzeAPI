namespace Economizze.Library
{
    public class QuotePrescription
    {
        public long QuoteId { get; set; }
        public long PrescriptionId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
