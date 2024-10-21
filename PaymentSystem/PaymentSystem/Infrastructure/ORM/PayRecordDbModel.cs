namespace PaymentSystem.Infrastructure.ORM
{
    public class PayRecordDbModel
    {
        public string Id { get; set; }
        public string EmpId { get; set; }
        public DateOnly PayDate { get; set; }
        public int Amount { get; set; }
    }
}
