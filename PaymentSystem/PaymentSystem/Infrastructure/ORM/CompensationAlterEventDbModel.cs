namespace PaymentSystem.Infrastructure.ORM
{
    public class CompensationAlterEventDbModel
    {
        public string EmpId { get; set; }
        public int Amount { get; set; }
        public DateOnly StartDate { get; set; }
    }
}
