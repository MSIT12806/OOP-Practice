using Microsoft.EntityFrameworkCore;
using PaymentSystem.Infrastructure.ORM;

namespace PaymentSystem.Adapter
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<EmpDbModel> Emps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmpDbModel>().HasIndex(e => e.Name);
        }
    }
}
