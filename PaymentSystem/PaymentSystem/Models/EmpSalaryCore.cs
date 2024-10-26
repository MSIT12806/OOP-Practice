using static PaymentSystem.Models.BasicDataMaintenece.Employee;

namespace PaymentSystem.Models
{
    public class EmpSalaryCore
    {

        public EmpSalaryCore(string empId, int salary)
        {
            EmpId = empId;
            Amount = salary;
        }
        public string EmpId { get; set; }
        public int Amount { get; set; }
    }
}
