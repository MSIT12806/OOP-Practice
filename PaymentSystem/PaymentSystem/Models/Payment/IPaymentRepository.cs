
namespace PaymentSystem.Models.Payment
{
    public interface IPaymentRepository
    {
        Employee Rebuild(string empId);
        string AddSalesReceipt(SalesReceipt salesReceipt);
        void DeleteSalesReceiptBy(string salesReceiptId);
        IEnumerable<SalesReceipt> GetSalesReceipts(string empId);

        void AddSalary(EmpSalary amountCore);
        IEnumerable<EmpSalary> GetSalaries();
        EmpSalary GetSalary(string empId);

        IEnumerable<ServiceCharge> GetServiceCharges(string id);
        void DeleteServiceChargeBy(string setviceChargeId);
        string AddServiceCharge(ServiceCharge serviceCharge);
    }
}
