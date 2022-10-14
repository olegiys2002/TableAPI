using BookingTables.Infrastructure.Views;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Table> Tables { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Avatar> Avatars { get; set; }
        public DbSet<UserAvatarsDTO> UserAvatars { get; set; }
        public ApplicationContext(DbContextOptions options) : base(options)
        {
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAvatarsDTO>().ToView(nameof(UserAvatars)).HasKey(user => user.Id);
        }
    }
}