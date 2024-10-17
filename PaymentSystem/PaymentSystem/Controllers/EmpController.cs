using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Adapter;
using PaymentSystem.Application.Emp;
using PaymentSystem.ViewModel;

namespace PaymentSystem.Controllers
{
    public class EmpController : Controller
    {
        private EmpMapper _empMapper;
        private EmpService _emp;

        public EmpController(EmpService service, EmpMapper mapper)
        {
            this._empMapper = mapper;
            this._emp = service;
        }
        public IActionResult EmpList()
        {
            var empList = _emp.GetList().Select(_empMapper.ToChgModel).ToList();
            return View();
        }

        public IActionResult AddEmp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmp(AddEmpViewModel addEmp)
        {
            _emp.AddEmp(_empMapper.ToCoreModel(addEmp));
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
