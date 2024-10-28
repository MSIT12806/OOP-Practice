using Microsoft.AspNetCore.Mvc;
using Payment.Application;
using Payment.Models.Payment;
using PaymentSystem.Adapter.Payment;

namespace PaymentSystem.Controllers
{
    public class SalaryController : PaymentControllerBase
    {
        public SalaryController(PaymentService service) : base(service)
        {
        }

        public IActionResult SetSalary(string empId)
        {
            var emp = protectedPaymentService.Rebuild(empId) as MounthlyEmployee;
            return View(emp.GetSalary());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetSalary(SalarySaveViewModel vm)
        {
            var emp = protectedPaymentService.Rebuild(vm.EmpId) as MounthlyEmployee;
            emp.SetSalary(vm.Salary, vm.StartDate.ToDateTime(TimeOnly.MinValue));

            return RedirectToAction(nameof(EmpController.ChgEmp), nameof(EmpController).Replace("Controller", ""));
        }
    }
}
