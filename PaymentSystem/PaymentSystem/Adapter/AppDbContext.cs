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
        public DbSet<ServiceChargeDbModel> ServiceCharges { get; set; }
        public DbSet<AmountDbModel> Amounts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmpDbModel>().HasKey(e => e.EmpId);
            modelBuilder.Entity<EmpDbModel>().Property(p => p.EmpId).IsRequired();
            modelBuilder.Entity<EmpDbModel>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<EmpDbModel>().Property(p => p.Address).IsRequired();

            modelBuilder.Entity<ServiceChargeDbModel>().HasKey(e => e.EmpId);
            modelBuilder.Entity<ServiceChargeDbModel>().HasIndex(e => e.EmpId);
            modelBuilder.Entity<ServiceChargeDbModel>().HasIndex(e => e.MemberId);

            modelBuilder.Entity<ServiceChargeDbModel>().Property(p => p.EmpId).IsRequired();
            modelBuilder.Entity<ServiceChargeDbModel>().Property(p => p.MemberId).IsRequired();
            modelBuilder.Entity<ServiceChargeDbModel>().Property(p => p.ServiceCharge).IsRequired();

            modelBuilder.Entity<ServiceChargeDbModel>()
                .HasOne<EmpDbModel>()
                .WithOne()
                .HasForeignKey<EmpDbModel>(x => x.EmpId);

            modelBuilder.Entity<AmountDbModel>().HasKey(e => e.EmpId);
            modelBuilder.Entity<AmountDbModel>()
                .HasOne<EmpDbModel>()
                .WithOne()
                .HasForeignKey<AmountDbModel>(x => x.EmpId);
        }
    }
}
