using DataBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataBackend.Database
{
    public class AppDBContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            try
            {
                Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
