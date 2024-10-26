
using PaymentSystem.Models.Payment;

namespace PaymentSystem.Models
{
    public interface IPaymentRepository
    {
        Employee Rebuild(string empId);
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
