using PaymentSystem.Application.Payday;
using PaymentSystem.Models;

namespace PaymentSystem.Adapter.Payday
{
    public class SalesReceiptSetter : ISalesReceiptSetter
    {
        AppDbContext _context;
        public SalesReceiptSetter(AppDbContext context)
        {
            _context = context;
        }

        public void SetSalesReceipt(IEnumerable<PaydayCore> paydays)
        {
            var salesReceipts = _context.SalesReceipts.ToList();

            foreach (var payday in paydays)
            {
                var salesReceipt = salesReceipts.FirstOrDefault(x => x.EmpId == payday.EmpId);

                if (salesReceipt != null)
                {
                    payday.SalesReceipt = salesReceipt.Amount;
                }
            }
        }
    }

}
