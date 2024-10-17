using PaymentSystem.Infrastructure.ORM;

namespace PaymentSystem.Models
{
    public interface IEmpRepository
    {
        void AddEmp(EmpDbModel empDbModel);
    }
}
