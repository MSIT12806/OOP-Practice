using static PaymentSystem.Models.Emp;

namespace PaymentSystem.Models
{
    public class EmpSalaryCore
    {
        public EmpSalaryCore(string empId, int salary, PayWayEnum payWay)
        {
            EmpId = empId;
            Amount = salary;
            PayWay = payWay;
        }
        public string EmpId { get; set; }
        public int Amount { get; set; }
        public PayWayEnum PayWay { get; set; }
    }
}
