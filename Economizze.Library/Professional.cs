namespace Economizze.Library
{
    public class Professional
    {
        public short ProfessionalId { get; set; }
        public short ProfessionId { get; set; }
        public string ProfessionalName { get; set; } = string.Empty;
        public string ProfessionalNameAscii { get; set; } = string.Empty;
        public string CounselNumber { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
