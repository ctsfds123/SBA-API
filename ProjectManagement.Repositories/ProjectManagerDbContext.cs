using ProjecManagement.EntityLayer;
using System.Data.Entity;

namespace ProjecManagement.Repositories
{
    public class ProjecManagementDbContext : DbContext
    {
        public ProjecManagementDbContext():base("name=ProjecManagementDb")
        {
        }

        public DbSet<ParentTask> ParentTasks { get; set; }

        public DbSet<ProjectDetails> Projects { get; set; }

        public DbSet<ProjectTaskDetails> Tasks { get; set; }

        public DbSet<UserDetails> UserDetails { get; set; }
    }
}
