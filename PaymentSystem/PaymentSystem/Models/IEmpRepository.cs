
namespace PaymentSystem.Models
{
    public interface IEmpRepository: IDisposable
    {
        bool Disposed { get; }
        void Add(EmpCore emp);
        IEnumerable<EmpCore> GetList();
        EmpCore Rebuild(string empId);
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
