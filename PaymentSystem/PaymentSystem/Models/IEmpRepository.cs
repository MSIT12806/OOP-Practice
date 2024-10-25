
namespace PaymentSystem.Models
{
    public interface IEmpRepository: IAsyncDisposable
    {
        void Add(Emp emp);
        IEnumerable<Emp> GetList();
        Emp Rebuild(string empId);
        void Update(Emp empCore);

        string AddSalesReceipt(SalesReceiptCore salesReceipt);
        void DeleteSalesReceiptBy(string salesReceiptId);
        IEnumerable<SalesReceiptCore> GetSalesReceipts(string empId);

        void AddSalary(EmpSalaryCore amountCore);
        IEnumerable<EmpSalaryCore> GetSalaries();
        EmpSalaryCore GetSalary(string empId);

        IEnumerable<ServiceChargeCore> GetServiceCharges(string id);
        void DeleteServiceChargeBy(string setviceChargeId);
        string AddServiceCharge(ServiceChargeCore serviceCharge);
    }
}
