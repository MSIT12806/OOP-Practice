namespace PaymentSystem.Models.Payment
{
    public class SalesReceipt
    {
        public string Id { get; set; }
        public string EmpId { get; set; }
        public DateOnly SalesDate { get; set; }
        public int Commission { get; set; }
    }
}
