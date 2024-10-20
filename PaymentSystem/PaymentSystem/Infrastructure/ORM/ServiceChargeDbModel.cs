namespace PaymentSystem.Infrastructure.ORM
{
    public class ServiceChargeDbModel
    {
        public string ServiceChargeId { get; internal set; }
        public string EmpId { get; internal set; }
        public int ServiceCharge { get; internal set; }
        public DateOnly ApplyDate { get; internal set; }
    }
}
