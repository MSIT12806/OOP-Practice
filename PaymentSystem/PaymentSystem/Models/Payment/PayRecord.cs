namespace PaymentSystem.Models.Payment
{
    public class PayRecord
    {
        public string Id { get; set; }
        public string EmpId { get; set; }
        public int Amount { get; set; }
        public DateOnly PayDate { get; set; }
    }
}
