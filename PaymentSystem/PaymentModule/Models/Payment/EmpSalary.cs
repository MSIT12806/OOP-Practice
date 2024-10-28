
namespace Payment.Models.Payment
{
    public class EmpSalary
    {

        public EmpSalary(string empId, int salary)
        {
            EmpId = empId;
            Amount = salary;
        }
        public string EmpId { get; set; }
        public int Amount { get; set; }
    }
}
