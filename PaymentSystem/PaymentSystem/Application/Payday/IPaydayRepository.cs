using PaymentSystem.Models;

namespace PaymentSystem.Application.Payday
{
    public interface IPaydayRepository
    {
        IEnumerable<PayRecordCore> GetPayRecordsBy(DateOnly date);
        TimeCardCore GetTimeCard(string timeCardId);
        IEnumerable<TimeCardCore> GetTimeCards(string empId);
    }
}
