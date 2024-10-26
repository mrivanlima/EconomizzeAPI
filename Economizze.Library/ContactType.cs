namespace Economizze.Library
{
    public class ContactType
    {
        public short ContactTypeId { get; set; }
        public string ContactTypeName { get; set; } = string.Empty;
        public string ContactTypeNameAscii { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
