using Payment.Models.Payment;
using PaymentSystem.Adapter.Payment;

namespace PaymentSystem.Adapter
{
    public static class SalesReceiptMapper
    {
        public static SalesReceiptQueryViewModel ToViewModel(SalesReceipt receipt)
        {
            return new SalesReceiptQueryViewModel
            {
                Id = receipt.Id,
                SalesDate = receipt.SalesDate,
                Commission = receipt.Commission
            };
        }

    }
}
