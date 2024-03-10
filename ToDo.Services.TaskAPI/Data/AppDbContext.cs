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
    }
}
