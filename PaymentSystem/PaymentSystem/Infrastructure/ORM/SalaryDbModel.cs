namespace PaymentSystem.Infrastructure.ORM
{
    public class SalaryDbModel
    {
        public string EmpId { get; set; }
        public int Amount { get; set; }
        public int PayWay { get; set; }
        public DateTime CreateDatetime { get; set; }
    }
}
