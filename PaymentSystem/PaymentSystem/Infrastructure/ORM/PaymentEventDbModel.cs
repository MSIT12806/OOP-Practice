namespace PaymentSystem.Infrastructure.ORM
{
    public class PaymentEventDbModel
    {
        public string Id { get; set; }
        public string EmpId { get; set; }
        public DateOnly PayDate { get; set; }
        public string CompensationType { get; internal set; }
    }
}
