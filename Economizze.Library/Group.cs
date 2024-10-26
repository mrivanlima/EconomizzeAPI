namespace Economizze.Library
{
    public class Group
    {
        public short GroupId { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public string GroupNameAscii { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
