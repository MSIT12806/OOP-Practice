using PaymentSystem.Controllers;
using PaymentSystem.Models;

namespace PaymentSystem.Adapter.Payday
{
    public static class PaydayMapper
    {
        public static EmpSalaryCore ToCore(SalarySaveViewModel vm)
        {
            return new EmpSalaryCore
            {
                EmpId = vm.EmpId,
                Salary = vm.Salary,
                PayWay = vm.PayWay
            };

        }

        public static SalarySaveViewModel ToSaveViewModel(EmpSalaryCore core)
        {
            return new SalarySaveViewModel
            {
                EmpId = core.EmpId,
                Salary = core.Salary,
                PayWay = core.PayWay
            };
        }
    }
}
