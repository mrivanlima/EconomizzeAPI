namespace Economizze.Library
{
    public class UserGroup
    {
        public short GroupId { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime GroupStartDate { get; set; }
        public DateTime GroupEndDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
