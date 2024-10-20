namespace PaymentSystem.Infrastructure.ORM
{
    public class TimeCardDbModel
    {
        public string EmpId { get; set; }
        public DateOnly WorkDate { get; set; }
        public int Hours { get; set; }
    }
}
