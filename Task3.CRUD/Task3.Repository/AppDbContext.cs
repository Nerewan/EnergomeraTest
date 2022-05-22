using Microsoft.EntityFrameworkCore;
using Task3.Model.Entities;

namespace Task3.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            Database.EnsureCreated();
        }
    }
}
