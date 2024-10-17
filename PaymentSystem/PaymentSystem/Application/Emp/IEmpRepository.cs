using PaymentSystem.Infrastructure.ORM;
using PaymentSystem.Models;

namespace PaymentSystem.Application.Emp
{
    public interface IEmpRepository
    {
        void AddEmp(EmpCore emp);
        IEnumerable<EmpCore> GetList();
    }
}
