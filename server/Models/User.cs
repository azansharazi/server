namespace server.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        // User can Create Multiple Projects 
       public ICollection<Project>? Projects { get; set; }
        public ICollection<ProjectMember>? ProjectMembers { get; set; } 
        // User can Create Multiple Tasks 
       public ICollection<TaskItem>? TaskItems { get; set; } 
        // Comments
       public ICollection<Comment>? Comments { get; set; } 
        // TaskAssigment Relation
        public ICollection<TaskAssignment>? Assignments { get; set; } 
        //UserRoles

        public ICollection<UserRole>? UserRoles { get; set; } 
    }
}
