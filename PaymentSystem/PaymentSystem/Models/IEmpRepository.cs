
namespace PaymentSystem.Models
{
    public interface IEmpRepository
    {
        void Add(EmpCore emp);
        IEnumerable<EmpCore> GetList();
        EmpCore GetSingle(string empId);
        void InjectData(EmpCore empCore);
        void Update(EmpCore empCore);

        string AddSalesReceipt(SalesReceiptCore salesReceipt);
        void DeleteSalesReceiptBy(string salesReceiptId);
        IEnumerable<SalesReceiptCore> GetSalesReceipts(string empId);

        void AddSalary(EmpSalaryCore amountCore);
        IEnumerable<EmpSalaryCore> GetEmpSalaries();
        EmpSalaryCore GetSalary(string empId);

        IEnumerable<ServiceChargeCore> GetServiceCharges(string id);
        void DeleteServiceChargeBy(string setviceChargeId);
        string AddServiceCharge(ServiceChargeCore serviceCharge);
    }
}
