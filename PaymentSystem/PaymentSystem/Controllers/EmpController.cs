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
            var empList = this._emp.GetList().Select(this._empMapper.ToChgModel).ToList();
            return this.View();
        }

        public IActionResult AddEmp()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmp(AddEmpViewModel addEmp)
        {
            this._emp.AddEmp(this._empMapper.ToCoreModel(addEmp));
            return this.RedirectToAction(nameof(ChgEmp), new { empId = addEmp.EmpId });
        }

        public IActionResult DelEmp()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DelEmp(DelEmpViewModel delEmp)
        {
            return this.RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""), new { empId = delEmp.EmpId });
        }

        public IActionResult ChgEmp(string empId)
        {
            if (string.IsNullOrEmpty(empId))
            {
                return this.View("Error", new ErrorViewModel { RequestId = empId });
            }

            var chgem = this._empMapper.ToChgModel(this._emp.GetSingle(empId));
            return this.View(chgem);
        }

    }
}
