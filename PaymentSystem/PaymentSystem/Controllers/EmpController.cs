using Microsoft.AspNetCore.Mvc;
using Payment.Application;
using PaymentSystem.Adapter.BasicDataMaintenence;
using PaymentSystem.ViewModel;

namespace PaymentSystem.Controllers
{
    public class EmpController : Controller
    {
        private EmpDataService _emp;
        private PaymentService _payment;

        public EmpController(EmpDataService service, PaymentService paymentService)
        {
            this._emp = service;
            this._payment = paymentService;
        }

        public async Task<IActionResult> AddEmp()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmp(AddEmpViewModel addEmp)
        {
            this._emp.Build(addEmp.EmpId, addEmp.Name, addEmp.Address);

            this._payment.SetSalaryAndPaymentDate(addEmp.EmpId, addEmp.PayWay, addEmp.Amount, addEmp.StartDate.ToDateTime(TimeOnly.MinValue));

            return this.RedirectToAction(nameof(ChgEmp), new { empId = addEmp.EmpId });
        }

        public async Task<IActionResult> DelEmp()
        {
            return this.View();
        }

        [HttpPost]
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
            var emp = this._emp.Rebuild(empId);
            var chgem = EmpMapper.ToChgModel(emp);
            return this.View(chgem);
        }
        [HttpPost]
        public async Task<IActionResult> ChgEmp(ChgEmpViewModel chgEmp)
        {
            this._emp.ChgEmpName(chgEmp.EmpId, chgEmp.Name);
                this._emp.ChgEmpAddress(chgEmp.EmpId, chgEmp.Address);
            return this.RedirectToAction(nameof(ChgEmp), new { empId = chgEmp.EmpId });
        }

    }
}
