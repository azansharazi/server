using Microsoft.VisualBasic;

namespace server.Models
{
    public class TaskItem
    {
        public int TastItemId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DueDate DueDate { get; set; }
        public string Status { get; set; } = string.Empty;
        //OwnerShip 
        public int CreatorId { get; set; }
        public User Creator { get; set; } = new User();
        //Project Relation 
        public int ProjectId { get; set; }
        public Project Project { get; set; } = new Project();
        // TaskAssigment Telation
        public ICollection<TaskAssignment> Assignments { get; set; } = new List<TaskAssignment>();
    }
}
