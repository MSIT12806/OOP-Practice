using static Payment.Models.BasicDataMaintenece.Employee;

namespace PaymentSystem.Infrastructure.ORM
{
    public class EmpDbModel
    {
        public string EmpId { get; internal set; }

        public string Name { get; internal set; }

        public string Address { get; internal set; }

        public string PayWay { get; internal set; }
    }
}
