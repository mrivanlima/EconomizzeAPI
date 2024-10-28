namespace Economizze.Library
{
    public class DrugstoreNeighborhoodSubscription
    {
        public int DrugstoreId { get; set; }
        public int NeighborhoodId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
