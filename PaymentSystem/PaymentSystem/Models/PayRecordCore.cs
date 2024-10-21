namespace PaymentSystem.Models
{
    public class PayRecordCore
    {
        public string Id { get; set; }
        public string EmpId { get; set; }
        public int Amount { get; set; }
        public DateOnly PayDate { get; set; }
    }
}
