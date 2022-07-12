using BackendTraineesTask1.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendTraineesTask1.Data
{
    public class ApplicationDataContext : DbContext
    {
    
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {
        }


        public DbSet<User> User { get; set; }
    }
}