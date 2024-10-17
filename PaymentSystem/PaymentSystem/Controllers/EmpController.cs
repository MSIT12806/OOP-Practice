using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Models;
using PaymentSystem.ViewModel;
using PaymentSystem.ViewModel.Emp;

namespace PaymentSystem.Controllers
{
    public class EmpController : Controller
    {
        private IEmpRepository _empRepository;
        private Emp _emp;

        public EmpController(IEmpRepository empRepository)
        {
            this._empRepository = empRepository;
            this._emp = new Emp(empRepository);
        }
        public IActionResult AddEmp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmp(AddEmpViewModel addEmp)
        {
            _emp.AddEmp(addEmp);
            return RedirectToAction(nameof(ChgEmp), new { empId = addEmp.EmpId });
        }

        public IActionResult DelEmp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DelEmp(DelEmpViewModel delEmp)
        {
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""), new { empId = delEmp.EmpId });
        }

        public IActionResult ChgEmp(string empId)
        {
            if (string.IsNullOrEmpty(empId))
            {
                return View("Error", new ErrorViewModel { RequestId = empId });
            }

            var chgem = new ChgEmpViewModel
            {
                EmpId = empId,
            };

            return View(chgem);
        }

    }
}
