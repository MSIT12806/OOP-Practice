namespace PaymentSystem.Application.Payday
{
    public interface ISalesReceiptSetter
    {
        void SetSalesReceipt(IEnumerable<Models.PaydayCore> paydays);
    }

}
