using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using server.Models;

namespace server.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base( options)
        {

            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ProjectMember> Managers { get; set; }
        public DbSet<TaskAssignment> Assignments { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        //Fluent api
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //user 
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);
                entity.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(100);
                entity.Property(u => u.UserEmail)
               .IsRequired()
               .HasMaxLength(150);

            });
            //Project 
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(p => p.ProjectId);
                entity.Property(p => p.ProjectName)
                .IsRequired()
                .HasMaxLength(200);
                entity.Property(p => p.ProjectDescription)
               .IsRequired()
               .HasMaxLength(1000);
                //Create relationship (User -> Projects)
                entity.HasOne(p => p.Creator)
                .WithMany(p => p.Projects)
                .HasForeignKey(p => p.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);
            });
            //TaskItem
            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.HasKey(t => t.TastItemId);
                entity.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);
                entity.Property(t => t.Status)
               .HasMaxLength(50);
                //Relationship (Project-TaskItem)
                entity.HasOne(t => t.Project)
                .WithMany(t => t.TaskItems)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
                // Creator relation (User -> Tasks)
                entity.HasOne(t => t.Creator)
                .WithMany(t => t.TaskItems)
                .HasForeignKey(t => t.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            });
            //Comments
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(c => c.CommentId);
                entity.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(1000);
               
                // Relationship (Comment - Task)
                entity.HasOne(c => c.TaskItem)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.TaskId)
                .OnDelete(DeleteBehavior.Cascade);
                // User RelationShip
                entity.HasOne(c => c.User)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.CommentId)
                .OnDelete(DeleteBehavior.Restrict);

            });
            //Project member 
            modelBuilder.Entity<ProjectMember>(entity =>
            {
                entity.HasKey(pm => new {pm.UserId, pm.ProjectId});
                entity.Property(pm => pm.Role)
                .IsRequired()
                .HasMaxLength(50);
               
                // Relationship (Comment - Task)
                entity.HasOne(pm => pm.User)
                .WithMany(pm => pm.ProjectMembers)
                .HasForeignKey(pm => pm.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                // User RelationShip
                entity.HasOne(pm => pm.Project)
                .WithMany(pm => pm.ProjectMembers)
                .HasForeignKey(pm => pm.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            });
            //Task Assignment
            modelBuilder.Entity<TaskAssignment>(entity =>
            {
                entity.HasKey(ta => new { ta.UserId, ta.TaskItemId });
                // Relationship (User - Assignment)
                entity.HasOne(ta => ta.User)
                .WithMany(ta => ta.Assignments)
                .HasForeignKey(ta => ta.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                //  RelationShip (TaskItem - Assignment)
                entity.HasOne(ta => ta.TaskItem)
                .WithMany(ta => ta.Assignments)
                .HasForeignKey(ta => ta.TaskItemId)
                .OnDelete(DeleteBehavior.Cascade);

            });
            //Role
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(r =>  r.RoleId);
                entity.Property(r =>r. RoleName)
                .IsRequired()
                .HasMaxLength(100);

            });
            //User Role 
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });
          
                // Relationship (User - Role)
                entity.HasOne(ur => ur.User)
                .WithMany(ur => ur.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                // Role RelationShip
                entity.HasOne(pm => pm.Role)
                .WithMany(pm => pm.UserRoles)
                .HasForeignKey(pm => pm.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            });

        }
    }
}
