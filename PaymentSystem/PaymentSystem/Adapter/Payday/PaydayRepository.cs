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
        public void Save(AmountCore amountCore)
        {
            AmountDbModel dbModel = this.ToDbModel(amountCore);

            this._appDbContext.Amounts.Add(dbModel);
            this._appDbContext.SaveChanges();
        }

        public IEnumerable<AmountCore> GetAmounts()
        {
            return this._appDbContext.Amounts.ToList().Select(this.ToCoreModel);
        }

        private AmountCore ToCoreModel(AmountDbModel source)
        {
            return new AmountCore
            {
                EmpId = source.EmpId,
                Salaried = source.Amount,
            };
        }

        private AmountDbModel ToDbModel(AmountCore amountCore)
        {
            return new AmountDbModel
            {
                EmpId = amountCore.EmpId,
                Amount = amountCore.Salaried,
            };
        }
    }
}
