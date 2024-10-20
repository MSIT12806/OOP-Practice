using PaymentSystem.Models;

namespace PaymentSystem.Application.Payday
{
    public interface IPaydayRepository
    {
        IEnumerable<EmpSalaryCore> GetEmpSalaries();
        TimeCardCore GetTimeCard(string timeCardId);
        IEnumerable<TimeCardCore> GetTimeCards(string empId);
        void Save(EmpSalaryCore amountCore);
    }
}
