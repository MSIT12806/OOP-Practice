
namespace Payment.Models.BasicDataMaintenece
{
    public class Employee
    {
        public string Id { get;  set; }

        public string Name { get;  set; }

        public string Address { get;  set; }

        public PayWayEnum PayWay { get;  set; }
        public enum PayWayEnum
        {
            None,
            Monthly,
            Sales,
            Union,
            Hourly
        }
    }
}
