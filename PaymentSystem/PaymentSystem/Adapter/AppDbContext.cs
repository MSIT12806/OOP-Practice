using Microsoft.EntityFrameworkCore;
using PaymentSystem.Infrastructure.ORM;
using System.Linq.Expressions;

namespace PaymentSystem.Adapter
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<EmpDbModel> Emps { get; set; }
        public DbSet<ServiceChargeDbModel> ServiceCharges { get; set; }
        public DbSet<SalaryDbModel> Salaries { get; set; }
        public DbSet<SalesReceiptDbModel> SalesReceipts { get; set; }
        public DbSet<TimeCardDbModel> TimeCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetEmp(modelBuilder);
            SetServiceCharge(modelBuilder);
            SetSalary(modelBuilder);
            SetSalesReceipt(modelBuilder);
        }

        private static void SetEmp(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmpDbModel>().HasKey(e => e.EmpId);
            modelBuilder.Entity<EmpDbModel>().Property(p => p.EmpId).IsRequired();
            modelBuilder.Entity<EmpDbModel>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<EmpDbModel>().Property(p => p.Address).IsRequired();
        }
        private static void SetServiceCharge(ModelBuilder modelBuilder)
        {
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
        }
        private static void SetSalary(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalaryDbModel>().HasKey(e => e.EmpId);
            modelBuilder.Entity<SalaryDbModel>()
                .HasOne<EmpDbModel>()
                .WithOne()
                .HasForeignKey<SalaryDbModel>(x => x.EmpId);
        }
        private static void SetSalesReceipt(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalesReceiptDbModel>().HasKey(e => e.EmpId);
            modelBuilder.Entity<SalesReceiptDbModel>().HasIndex(e => e.EmpId);
        }
        private static void SetTimeCard(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeCardDbModel>().HasKey(e => e.EmpId);
            modelBuilder.Entity<TimeCardDbModel>().HasIndex(e => e.EmpId);
        }

        public TEntity Update<TEntity>(TEntity dbSource, TEntity updateObject) where TEntity : class
        {
            Entry(dbSource).CurrentValues.SetValues(updateObject);
            return base.Update(dbSource).Entity;
        }
    }

}
