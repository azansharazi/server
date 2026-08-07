namespace server.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        //UserRoles

        public ICollection<UserRole>? UserRoles { get; set; }
    }
}
