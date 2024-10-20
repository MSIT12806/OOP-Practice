﻿using Microsoft.AspNetCore.Mvc;
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

        public IActionResult GetEmp()
        {
            var empList = this._emp.GetList().Select(EmpMapper.ToInfoModel).ToList();
            return this.View(empList);
        }

        public IActionResult AddEmp()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmp(AddEmpViewModel addEmp)
        {
            this._emp.AddEmp(EmpMapper.ToCoreModel(addEmp));
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

            var chgem = EmpMapper.ToChgModel(this._emp.GetSingle(empId));
            return this.View(chgem);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChgEmp(ChgEmpViewModel chgEmp)
        {
            this._emp.ChgEmp(EmpMapper.ToCoreModel(chgEmp));
            return this.RedirectToAction(nameof(ChgEmp), new { empId = chgEmp.EmpId });
        }

    }
}
