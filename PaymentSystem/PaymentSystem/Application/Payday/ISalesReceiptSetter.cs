using PaymentSystem.Models;

namespace PaymentSystem.Application.Payday
{
    public interface ISalesReceiptSetter
    {
        void SetSalesReceipt(IEnumerable<Payroll> paydays);
    }

}
