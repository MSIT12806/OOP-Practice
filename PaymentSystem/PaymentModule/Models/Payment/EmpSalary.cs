
namespace Payment.Models.Payment
{
    public class EmpSalary
    {

        public EmpSalary(string empId, int salary, string employeeType)
        {
            EmpId = empId;
            Amount = salary;
            EmployeeType = employeeType;
        }
        public string EmpId { get; set; }
        public int Amount { get; set; }
        public string EmployeeType { get; set; }
    }
}
