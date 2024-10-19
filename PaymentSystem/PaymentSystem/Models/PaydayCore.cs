namespace PaymentSystem.Models
{
    public class PaydayCore
    {
        public string EmpId { get; set; }
        public int Salary { get; set; }
        public int? ServiceCharge { get; set; }
        public int? SalesReceipt { get; set; }
        public int ShouldPay => this.Salary - (this.ServiceCharge ?? 0) + (this.SalesReceipt ?? 0);
    }

}
