namespace PaymentSystem.Application.Payday
{
    public class PaydayResult
    {
        public string EmpId { get; set; }
        public int Salary { get; set; }
        public int? ServiceCharge { get; set; }
        public int ShouldPay => Salary - (ServiceCharge ?? 0);
    }

}
