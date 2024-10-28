namespace Economizze.Library
{
    public class Drugstore
    {
        public int DrugstoreId { get; set; }
        public string DrugstoreName { get; set; } = string.Empty;
        public string DrugstoreNameAscii { get; set; } = string.Empty;
        public int AddressId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
