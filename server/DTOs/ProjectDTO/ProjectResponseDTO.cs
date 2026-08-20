using System.Reflection.Metadata;

namespace server.DTOs.ProjectDTO
{
    public class ProjectResponseDTO
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CreatorName { get; set; } = string.Empty;
    }
}
