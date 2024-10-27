using PaymentSystem.Models;

namespace PaymentSystem.Adapter
{
    public static class SalesReceiptMapper
    {
        public static SalesReceiptQueryViewModel ToViewModel(SalesReceipt receipt)
        {
            return new SalesReceiptQueryViewModel
            {
                Id = receipt.Id,
                SalesDate = receipt.SalesDate.ToDateTime(TimeOnly.MinValue),
                Commission = receipt.Commission
            };
        }

    }
}
