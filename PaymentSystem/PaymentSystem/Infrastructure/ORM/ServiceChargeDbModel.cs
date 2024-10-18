namespace PaymentSystem.Infrastructure.ORM
{
    public class ServiceChargeDbModel
    {

        public string EmpId { get; internal set; }

        public string MemberId { get; internal set; }

        public int ServiceCharge { get; internal set; }
    }
}
