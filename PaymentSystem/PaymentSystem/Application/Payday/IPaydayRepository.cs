using PaymentSystem.Models;

namespace PaymentSystem.Application.Payday
{
    public interface IPaydayRepository
    {
        IEnumerable<EmpSalaryCore> GetEmpSalaries();
        void Save(EmpSalaryCore amountCore);
    }
}
