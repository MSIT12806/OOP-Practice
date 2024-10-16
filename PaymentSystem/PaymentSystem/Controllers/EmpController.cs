using Microsoft.AspNetCore.Mvc;
using PaymentSystem.ViewModel;
using PaymentSystem.ViewModel.Emp;

namespace PaymentSystem.Controllers
{
    public class EmpController : Controller
    {
        public IActionResult AddEmp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmp(AddEmpViewModel addEmp)
        {
            return RedirectToAction(nameof(ChgEmp),new {empId = addEmp.EmpId});
        }

        public IActionResult DelEmp()
        {
            return View();
        }

        public IActionResult ChgEmp(string empId)
        {
            if(string.IsNullOrEmpty(empId))
            {
                return View("Error", new ErrorViewModel {RequestId = empId });
            }

            var chgem = new ChgEmpViewModel
            {
                EmpId = empId,
            };

            return View(chgem);
        }

    }
}
