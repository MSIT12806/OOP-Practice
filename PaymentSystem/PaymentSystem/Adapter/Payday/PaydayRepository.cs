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
            var dbModel = this._appDbContext.Salaries.SingleOrDefault(x => x.EmpId == amountCore.EmpId);
            if (dbModel == null)
            {
                dbModel = this.ToDbModel(amountCore);
                this._appDbContext.Salaries.Add(dbModel);
                this._appDbContext.SaveChanges();
                return;
            }

            if (this._appDbContext.Salaries.Any(x => x.EmpId == amountCore.EmpId))
            {
                this._appDbContext.Update(dbModel, this.ToDbModel(amountCore));
                this._appDbContext.SaveChanges();
                return;
            }
        }

        public IEnumerable<PayRecordCore> GetPayRecordsBy(DateOnly date)
        {
            return this._appDbContext.PayRecords.Where(x => x.PayDate == date).ToList().Select(this.ToCoreModel);
        }
        public IEnumerable<EmpSalaryCore> GetEmpSalaries()
        {
            return this._appDbContext.Salaries.ToList().Select(this.ToCoreModel);
        }

        public IEnumerable<TimeCardCore> GetTimeCards(string empId)
        {
            return this._appDbContext.TimeCards.Where(x => x.EmpId == empId).ToList().Select(this.ToCoreModel);
        }

        public TimeCardCore GetTimeCard(string timeCardId)
        {
            return this.ToCoreModel(this._appDbContext.TimeCards.SingleOrDefault(x => x.EmpId == timeCardId));
        }

        private TimeCardCore ToCoreModel(TimeCardDbModel source)
        {
            return new TimeCardCore
            {
                EmpId = source.EmpId,
                WorkDate = source.WorkDate,
                Hours = source.Hours,
            };
        }

        private PayRecordCore ToCoreModel(PayRecordDbModel source)
        {
            return new PayRecordCore
            {
                EmpId = source.EmpId,
                PayDate = source.PayDate,
                Amount = source.Amount,
            };
        }

        private TimeCardDbModel ToDbModel(TimeCardCore timeCardCore)
        {
            return new TimeCardDbModel
            {
                EmpId = timeCardCore.EmpId,
                WorkDate = timeCardCore.WorkDate,
                Hours = timeCardCore.Hours,
            };
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
