using PaymentSystem.Infrastructure.ORM;
using PaymentSystem.Models;

namespace PaymentSystem.Application.Emp
{
    public interface IEmpRepository
    {
        void Add(EmpCore emp);
        string AddSalesReceipt(SalesReceiptCore salesReceipt);
        void DeleteSalesReceiptBy(string salesReceiptId);
        IEnumerable<EmpCore> GetList();
        IEnumerable<SalesReceiptCore> GetSalesReceipts(string empId);
        EmpCore GetSingle(string empId);
        void Update(EmpCore empCore);
    }
}
