using static PaymentSystem.Models.Payment.Employee;

namespace PaymentSystem.Models.BasicDataMaintenece
{
    public class Employee
    {
        public string Id { get; internal set; }

        public string Name { get; internal set; }

        public string Address { get; internal set; }

        public PayWayEnum PayWay { get; internal set; }
        public enum PayWayEnum
        {
            Monthly,
            Sales,
            Union,
            Hourly
        }
    }
}
