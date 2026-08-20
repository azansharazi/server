using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.DTOs.ProjectDTO;
using server.Models;
using System.Security.Claims;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public ProjectsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Create Project

        [HttpPost("create")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> CreateProject(CreateProjectDTO dto)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var projects = new Project
            {
                ProjectName = dto.ProjectName,
                ProjectDescription = dto.Description,
                CreatorId = userId

            };
            await _dbContext.Projects.AddAsync(projects);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Projects Create", projects.ProjectId });
        }
        //Update Project
        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<ActionResult> UpdateProjectById(int id,CreateProjectDTO dto)
        {

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var project = await _dbContext.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            if (project.CreatorId != userId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            project.ProjectName = dto.ProjectName;
            project.ProjectDescription = dto.Description;
            
            
            await _dbContext.SaveChangesAsync();
            return Ok(project);
        }

        //Delete Project
        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<ActionResult> DeleteProjectById(int id)
        {

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var project = await _dbContext.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            if (project.CreatorId != userId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }
            _dbContext.Projects.Remove(project);
            await _dbContext.SaveChangesAsync();
            return Ok("Projects Deleted");
        }



        //get Projects
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<ActionResult> GetProjectById(int id)
        {

            var project = await _dbContext
                .Projects
                .AsNoTracking()
                .Select(p => new ProjectResponseDTO
                {
                ProjectName = p.ProjectName,
                ProjectId = p.ProjectId,
                Description = p.ProjectDescription,
                CreatorName = p.Creator!.UserName
               })
                .FirstOrDefaultAsync(p => p.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }


        //get All Projects 
        [HttpGet("All")]
        [Authorize]
        public async Task<ActionResult> GetProjects()
        {

            var project = await _dbContext
                .Projects
                .AsNoTracking()
                .Select(p => new ProjectResponseDTO
                {
                    ProjectName = p.ProjectName,
                    ProjectId = p.ProjectId,
                    Description = p.ProjectDescription,
                    CreatorName = p.Creator!.UserName
                })
                .ToListAsync();
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

    }
}
