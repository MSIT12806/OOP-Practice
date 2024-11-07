using IdentityModule.Interface;

namespace PaymentSystem.Adapter.IdentityValidation
{
    public class IdentityDataAccess : IDataAccess
    {
        private AppDbContext _context;
        public IdentityDataAccess(AppDbContext context)
        {
            this._context = context;
        }

        public void Delete<T>(T data) where T : class, IHasId
        {
            var entitySet = this._context.Set<T>();

            // 檢查 data 是否存在於 entitySet 中
            if (entitySet.Any(i => data.Id == i.Id))
            {
                entitySet.Remove(data);
                this._context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("The provided entity is not part of the DbSet.");
            }
        }

        IEnumerable<T> IDataAccess.GetList<T>(Func<T, bool> cond)
        {
            var entitySet = this._context.Set<T>();
            return entitySet.Where(cond);
        }

        public T GetOne<T>(Func<T, bool> cond) where T : class, IHasId
        {
            var entitySet = this._context.Set<T>();
            return entitySet.FirstOrDefault(cond);
        }

        public void Insert<T>(T data) where T : class, IHasId
        {
            var entitySet = this._context.Set<T>();
            entitySet.Add(data);
            this._context.SaveChanges();
        }

        public void Update<T>(T data) where T : class, IHasId
        {
            var entitySet = this._context.Set<T>();

            if (entitySet.Any(i => data.Id == i.Id))
            {
                entitySet.Update(data);
                this._context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("The provided entity is not part of the DbSet.");
            }
        }
    }
}
