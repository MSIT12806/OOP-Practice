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
        public DbSet<CompensationAlterEventDbModel> CompensationAlterEvents { get; set; }
        public DbSet<SalesReceiptDbModel> SalesReceipts { get; set; }
        public DbSet<TimeCardDbModel> TimeCards { get; set; }
        public DbSet<PaymentEventDbModel> PaymentEvents { get; set; }
        public DbSet<PayrollDbModel> Payrolls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetEmp(modelBuilder);
            SetServiceCharge(modelBuilder);
            SetCompensationAlterEvent(modelBuilder);
            SetSalesReceipt(modelBuilder);
            SetTimeCard(modelBuilder);
            SetPaymentEvent(modelBuilder);
            SetPayroll(modelBuilder);
        }

        private void SetPaymentEvent(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentEventDbModel>().HasKey(e => e.Id);
            modelBuilder.Entity<PaymentEventDbModel>().Property(p => p.Id).IsRequired();
            modelBuilder.Entity<PaymentEventDbModel>().Property(p => p.EmpId).IsRequired();
            modelBuilder.Entity<PaymentEventDbModel>().Property(p => p.PayDate).IsRequired();
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
            modelBuilder.Entity<ServiceChargeDbModel>().HasKey(e => e.ServiceChargeId);
            modelBuilder.Entity<ServiceChargeDbModel>().HasIndex(e => e.ServiceChargeId);

            modelBuilder.Entity<ServiceChargeDbModel>().HasIndex(e => e.EmpId);

            modelBuilder.Entity<ServiceChargeDbModel>().Property(p => p.EmpId).IsRequired();
            modelBuilder.Entity<ServiceChargeDbModel>().Property(p => p.ServiceChargeId).IsRequired();
            modelBuilder.Entity<ServiceChargeDbModel>().Property(p => p.ServiceCharge).IsRequired();

            modelBuilder.Entity<ServiceChargeDbModel>()
                .HasOne<EmpDbModel>()
                .WithMany()
                .HasForeignKey(x => x.EmpId);
        }
        private static void SetCompensationAlterEvent(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompensationAlterEventDbModel>().HasKey(e => e.EmpId);
            modelBuilder.Entity<CompensationAlterEventDbModel>()
                .HasOne<EmpDbModel>()
                .WithOne()
                .HasForeignKey<CompensationAlterEventDbModel>(x => x.EmpId);
        }
        private static void SetSalesReceipt(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalesReceiptDbModel>().HasKey(e => e.Id);
            modelBuilder.Entity<SalesReceiptDbModel>().HasIndex(e => e.EmpId);
        }
        private static void SetTimeCard(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeCardDbModel>().HasKey(e => e.Id);
            modelBuilder.Entity<TimeCardDbModel>().HasIndex(e => e.EmpId);
        }
        private static void SetPayroll(ModelBuilder modelBuilder)
        {
            // TODO: 思考：因為是 record，所以不要有任何 ForeignKey? 不想要被連帶刪除
            modelBuilder.Entity<PayrollDbModel>().HasKey(e => e.Id);
            modelBuilder.Entity<PayrollDbModel>().Property(p => p.Id).IsRequired();
            modelBuilder.Entity<PayrollDbModel>().Property(p => p.EmpId).IsRequired();
            modelBuilder.Entity<PayrollDbModel>().Property(p => p.PayDate).IsRequired();
            modelBuilder.Entity<PayrollDbModel>().Property(p => p.Amount).IsRequired();
        }

        public TEntity Update<TEntity>(TEntity dbSource, TEntity updateObject) where TEntity : class
        {
            Entry(dbSource).CurrentValues.SetValues(updateObject);
            return base.Update(dbSource).Entity;
        }

    }

}
