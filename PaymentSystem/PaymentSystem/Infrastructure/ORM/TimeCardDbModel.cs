namespace PaymentSystem.Infrastructure.ORM
{
    public class TimeCardDbModel
    {
        public string Id { get; set; }
        public string EmpId { get; set; }
        public DateOnly WorkDate { get; set; }
        public int Hours { get; set; }
    }
}
