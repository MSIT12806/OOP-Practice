using PaymentSystem.Infrastructure.ORM;
using PaymentSystem.Models;

namespace PaymentSystem.Application
{
    public class EmpRepository : IEmpRepository
    {
        private AppDbContext _appDbContext;

        public EmpRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddEmp(EmpDbModel empDbModel)
        {
            _appDbContext.Emps.Add(empDbModel);
            _appDbContext.SaveChanges();
        }
    }
}
