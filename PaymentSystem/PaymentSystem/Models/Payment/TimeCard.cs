namespace PaymentSystem.Models.Payment
{
    public class TimeCard
    {
        public string EmpId { get; set; }
        public DateOnly WorkDate { get; set; }
        public int Hours { get; set; }
    }
}
