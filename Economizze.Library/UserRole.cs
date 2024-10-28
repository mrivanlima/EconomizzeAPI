namespace Economizze.Library
{
    public class UserRole
    {
        public short RoleId { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime RoleStartDate { get; set; }
        public DateTime RoleEndDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
