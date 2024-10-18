using PaymentSystem.Models;

namespace PaymentSystem.Application.Payday
{
    public interface IPaydayRepository
    {
        IEnumerable<AmountCore> GetAmounts();
        void Save(AmountCore amountCore);
    }
}
