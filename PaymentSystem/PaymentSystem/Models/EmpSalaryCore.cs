namespace PaymentSystem.Models
{
    public class EmpSalaryCore
    {
        public enum PayWayEnum
        {
            Monthly,
            Hourly
        }
        public string EmpId { get; set; }
        public int Salary { get; set; }
        public PayWayEnum PayWay { get; set; }
    }
}
