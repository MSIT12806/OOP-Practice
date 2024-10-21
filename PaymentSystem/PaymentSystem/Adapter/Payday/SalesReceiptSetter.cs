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
                var salesReceiptByEmpId = salesReceipts.Where(x => x.EmpId == payday.EmpId);

                if (salesReceiptByEmpId != null && salesReceiptByEmpId.Count()!=0)
                {
                    payday.SalesReceipt = salesReceiptByEmpId.Sum(i => i.Amount);
                }
            }
        }
    }

}
