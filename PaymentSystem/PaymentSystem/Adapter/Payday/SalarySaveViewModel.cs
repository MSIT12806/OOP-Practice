using static PaymentSystem.Models.EmpSalaryCore;

namespace PaymentSystem.Adapter.Payday
{
    public class SalarySaveViewModel
    {
        public string EmpId { get; set; }
        public PayWayEnum PayWay { get; set; }
        public int Salary { get; set; }
    }
}
