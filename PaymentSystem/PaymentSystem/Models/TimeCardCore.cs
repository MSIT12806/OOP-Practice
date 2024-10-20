namespace PaymentSystem.Models
{
    public class TimeCardCore
    {
        public string EmpId { get; set; }
        public DateOnly WorkDate { get; set; }
        public int Hours { get; set; }
    }
}
