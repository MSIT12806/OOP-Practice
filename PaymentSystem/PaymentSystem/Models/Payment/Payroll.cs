namespace PaymentSystem.Models.Payment
{
<<<<<<<< HEAD:PaymentSystem/PaymentSystem/Models/Payment.cs
    public class Payment
========
    public class Payroll
>>>>>>>> refactor/重新反思領域模型:PaymentSystem/PaymentSystem/Models/Payment/Payroll.cs
    {
        public string EmpId { get; set; }
        public int Salary { get; set; }
        public int? ServiceCharge { get; set; }
        public int? SalesReceipt { get; set; }
        public int ShouldPay => this.Salary - (this.ServiceCharge ?? 0) + (this.SalesReceipt ?? 0);
    }
}
