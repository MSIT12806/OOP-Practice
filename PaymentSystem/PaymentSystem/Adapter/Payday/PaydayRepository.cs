using PaymentSystem.Application.Payday;
using PaymentSystem.Infrastructure.ORM;
using PaymentSystem.Models;

namespace PaymentSystem.Adapter.Payday
{
    public class PaydayRepository : IPaydayRepository
    {
        private AppDbContext _appDbContext;
        public PaydayRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public void Save(EmpSalaryCore amountCore)
        {
            SalaryDbModel dbModel = this.ToDbModel(amountCore);

            this._appDbContext.Salaries.Add(dbModel);
            this._appDbContext.SaveChanges();
        }

        public IEnumerable<EmpSalaryCore> GetEmpSalaries()
        {
            return this._appDbContext.Salaries.ToList().Select(this.ToCoreModel);
        }

        private EmpSalaryCore ToCoreModel(SalaryDbModel source)
        {
            return new EmpSalaryCore
            {
                EmpId = source.EmpId,
                Salary = source.Amount,
            };
        }

        private SalaryDbModel ToDbModel(EmpSalaryCore amountCore)
        {
            return new SalaryDbModel
            {
                EmpId = amountCore.EmpId,
                Amount = amountCore.Salary,
            };
        }
    }
}
