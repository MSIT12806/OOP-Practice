namespace PaymentSystem.Models
{
    public class SalesReceiptCore
    {
        public string Id { get; set; }
        public string EmpId { get; set; }
        public DateOnly SalesDate { get; set; }
        public int Commission { get; set; }
    }
}
