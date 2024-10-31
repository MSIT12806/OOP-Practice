namespace PaymentSystem.Infrastructure.ORM
{
    public class PayrollDbModel
    {
        public string Id { get; set; }
        public string EmpId { get; set; }
        public DateTime PayDate { get; set; }
        public int Amount { get; set; }
    }

    public class PayrollDetailDbModel
    {
        public string PayrollId { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
    }
}
