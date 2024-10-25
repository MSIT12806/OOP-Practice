using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Adapter;
using PaymentSystem.Application.Emp;
using PaymentSystem.ViewModel;

namespace PaymentSystem.Controllers
{
    public class EmpController : Controller
    {
        private EmpService _emp;

        public EmpController(EmpService service)
        {
            this._emp = service;
        }

        public async Task<IActionResult> AddEmp()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmp(AddEmpViewModel addEmp)
        {
            this._emp.Build(addEmp.EmpId, addEmp.Name, addEmp.Address, Models.Emp.PayWayEnum.Monthly);
            return this.RedirectToAction(nameof(ChgEmp), new { empId = addEmp.EmpId });
        }

        public async Task<IActionResult> DelEmp()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DelEmp(DelEmpViewModel delEmp)
        {
            return this.RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""), new { empId = delEmp.EmpId });
        }

        public async Task<IActionResult> ChgEmp(string empId)
        {
            if (string.IsNullOrEmpty(empId))
            {
                return this.View("Error", new ErrorViewModel { RequestId = empId });
            }

            var chgem = EmpMapper.ToChgModel(this._emp.Rebuild(empId));
            return this.View(chgem);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChgEmp(ChgEmpViewModel chgEmp)
        {
            this._emp.ChgEmpName(chgEmp.EmpId, chgEmp.Name);
                this._emp.ChgEmpAddress(chgEmp.EmpId, chgEmp.Address);
            return this.RedirectToAction(nameof(ChgEmp), new { empId = chgEmp.EmpId });
        }

    }
}
