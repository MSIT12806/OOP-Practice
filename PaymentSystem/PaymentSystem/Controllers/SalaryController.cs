using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Adapter.Payday;
using PaymentSystem.Application.Payday;

namespace PaymentSystem.Controllers
{
    public class SalaryController : PayControllerBase
    {
        public SalaryController(PaydayService service) : base(service)
        {
        }

        public IActionResult SetSalary(string empId)
        {
            var emp = protectedPaydayService.GetEmpSalary(empId);
            return View(emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetSalary(SalarySaveViewModel vm)
        {
            protectedPaydayService.SetSalary(PaydayMapper.ToCore(vm));
            return RedirectToAction(nameof(EmpController.ChgEmp), nameof(EmpController).Replace("Controller", ""));
        }
    }
}
