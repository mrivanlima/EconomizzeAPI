namespace Economizze.Library
{
    public class Profession
    {
        public short ProfessionId { get; set; }
        public string ProfessionName { get; set; } = string.Empty;
        public string ProfessionNameAscii { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
