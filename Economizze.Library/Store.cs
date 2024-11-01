namespace Economizze.Library
{
    public class Store
    {
        public int StoreId { get; set; }
        public Guid StoreUniqueId { get; set; }
        public string Cnpj { get; set; } = string.Empty;
        public string StoreName { get; set; } = string.Empty;
        public string StoreNameAscii { get; set; } = string.Empty;
        public int StoreAddressId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
