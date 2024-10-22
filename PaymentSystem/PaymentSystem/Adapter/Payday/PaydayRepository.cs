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

        public IEnumerable<TimeCardCore> GetTimeCards(string empId)
        {
            return this._appDbContext.TimeCards.Where(x => x.EmpId == empId).ToList().Select(this.ToCoreModel);
        }

        public TimeCardCore GetTimeCard(string timeCardId)
        {
            return this.ToCoreModel(this._appDbContext.TimeCards.SingleOrDefault(x => x.EmpId == timeCardId));
        }
        public IEnumerable<PayRecordCore> GetPayRecordsBy(DateOnly date)
        {
            return this._appDbContext.PayRecords.Where(x => x.PayDate == date).ToList().Select(this.ToCoreModel);
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


    }
}
