using IdentityModule.Implement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using PaymentSystem.Infrastructure.ORM;
using System.Collections.Concurrent;

namespace PaymentSystem.Adapter
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            this.Emps = new DbSetWithCache<EmpDbModel>(this.Set<EmpDbModel>(), i => i.EmpId);
        }
        public DbSetWithCache<EmpDbModel> Emps { get; set; }
        private DbSet<EmpDbModel> _Emps { get; set; }
        public DbSet<ServiceChargeDbModel> ServiceCharges { get; set; }
        public DbSet<CompensationAlterEventDbModel> CompensationAlterEvents { get; set; }
        public DbSet<SalesReceiptDbModel> SalesReceipts { get; set; }
        public DbSet<TimeCardDbModel> TimeCards { get; set; }
        public DbSet<PaymentPlanDbModel> PaymentPlans { get; set; }
        public DbSet<PayrollDbModel> Payrolls { get; set; }
        public DbSet<PayrollDetailDbModel> PayrollDetails { get; set; }

        #region Identity

        public DbSet<AspNetUser> DefaultUsers { get; set; }
        public DbSet<AspNetRole> DefaultRoles { get; set; }
        public DbSet<AspNetUserRole> DefaultUserRoles { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetEmp(modelBuilder);
            SetServiceCharge(modelBuilder);
            SetCompensationAlterEvent(modelBuilder);
            SetSalesReceipt(modelBuilder);
            SetTimeCard(modelBuilder);
            SetPaymentEvent(modelBuilder);
            SetPayroll(modelBuilder);
            SetPayrollDetail(modelBuilder);


            this.SetUsers(modelBuilder);
            this.SetRoles(modelBuilder);
            this.SetUserRoles(modelBuilder);
        }

        private static void SetPayrollDetail(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PayrollDetailDbModel>().HasKey(e => new { e.PayrollId, e.Description, e.Amount });
            modelBuilder.Entity<PayrollDetailDbModel>().Property(p => p.PayrollId).IsRequired();
            modelBuilder.Entity<PayrollDetailDbModel>().Property(p => p.Description).IsRequired();
            modelBuilder.Entity<PayrollDetailDbModel>().Property(p => p.Amount).IsRequired();
        }

        private static void SetPaymentEvent(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentPlanDbModel>().HasKey(e => e.Id);
            modelBuilder.Entity<PaymentPlanDbModel>().Property(p => p.Id).IsRequired();
            modelBuilder.Entity<PaymentPlanDbModel>().Property(p => p.EmpId).IsRequired();
            modelBuilder.Entity<PaymentPlanDbModel>().Property(p => p.PayDate).IsRequired();
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
            this.Entry(dbSource).CurrentValues.SetValues(updateObject);
            return base.Update(dbSource).Entity;
        }

        private void SetUserRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetUserRole>().HasKey(x => x.Id);
        }

        private void SetRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>().HasKey(x => x.Id);
        }

        private void SetUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetUser>().HasKey(x => x.Id);
        }


    }


    public class DbSetWithCache<TEntity> : DbSet<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly ConcurrentDictionary<string, TEntity> _cache;

        private readonly Func<TEntity, string> getKey;
        public DbSetWithCache(DbSet<TEntity> dbSet, Func<TEntity, string> getIdFunc)
        {
            this._dbSet = dbSet;
            this._cache = new ConcurrentDictionary<string, TEntity>();
            this.getKey = getIdFunc;
        }

        public override IEntityType EntityType => this._dbSet.EntityType;

        // 包裝 Add 方法，將實體添加到字典中
        public override EntityEntry<TEntity> Add(TEntity entity)
        {
            var key = this.getKey(entity);
            this._cache.TryAdd(key, entity);
            return this._dbSet.Add(entity);
        }

        // 查詢方法，先查字典再查資料庫
        public TEntity FindById(string id)
        {
            if (this._cache.TryGetValue(id, out TEntity cachedEntity))
            {
                return cachedEntity;
            }

            var entity = this._dbSet.Find(id);
            return entity;
        }

        // 其他需要的方法可以類似包裝
    }
}
