using Microsoft.EntityFrameworkCore;
using ToDo.Services.TaskAPI.Models;
using Task = ToDo.Services.TaskAPI.Models.Task;

namespace ToDo.Services.TaskAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region data for seed
            modelBuilder.Entity<Task>().HasData(
                new Task
                {
                    Id = 1,
                    Title = "Complete Project Proposal",
                    Description = "Draft and finalize the project proposal for client review.",
                    IsCompleted = false,
                    DueDate = DateTime.Now.AddDays(7),
                    Priority = PriorityLevel.High
                },
                new Task
                {
                    Id = 2,
                    Title = "Attend Team Meeting",
                    Description = "Participate in the weekly team meeting to discuss project updates.",
                    IsCompleted = false,
                    DueDate = DateTime.Now.AddDays(2),
                    Priority = PriorityLevel.Medium
                },
                new Task
                {
                    Id = 3,
                    Title = "Prepare Presentation",
                    Description = "Create a presentation for the upcoming client meeting.",
                    IsCompleted = false,
                    DueDate = DateTime.Now.AddDays(5),
                    Priority = PriorityLevel.High
                });
            #endregion
        }
    }
}
