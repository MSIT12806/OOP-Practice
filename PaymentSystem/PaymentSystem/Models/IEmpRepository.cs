
namespace PaymentSystem.Models
{
    public interface IEmpRepository
    {
        void Add(EmpCore emp);
        string AddSalesReceipt(SalesReceiptCore salesReceipt);
        void DeleteSalesReceiptBy(string salesReceiptId);
        IEnumerable<EmpCore> GetList();
        IEnumerable<SalesReceiptCore> GetSalesReceipts(string empId);
        EmpCore GetSingle(string empId);
        void InjectData(EmpCore empCore);
        void Update(EmpCore empCore);

        void AddSalary(EmpSalaryCore amountCore);
        IEnumerable<EmpSalaryCore> GetEmpSalaries();
        EmpSalaryCore GetSalary(string empId);
        IEnumerable<ServiceChargeCore> GetServiceCharges(string id);
    }
}
