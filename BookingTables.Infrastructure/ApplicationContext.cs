using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Table> Tables { get; set; }
        public DbSet<Order> Orders { get; set; }
        public ApplicationContext(DbContextOptions options) : base(options)
        {
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}