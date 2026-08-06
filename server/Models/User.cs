namespace server.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        // User can Create Multiple Projects 
       public ICollection<Project> Projects { get; set; } = new List<Project>();
        // ProjectMamber Relation
        public ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();
        // User can Create Multiple Tasks 
       public ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
        // Comments
       public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        // TaskAssigment Telation
        public ICollection<TaskAssignment> Assignments { get; set; } = new List<TaskAssignment>();


    }
}
