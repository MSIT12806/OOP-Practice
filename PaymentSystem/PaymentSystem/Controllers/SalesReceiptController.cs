using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Adapter;
using PaymentSystem.Application.Emp;
using PaymentSystem.ViewModel;

namespace PaymentSystem.Controllers
{
    public class SalesReceiptController : Controller
    {
        private EmpService _emp;

        public SalesReceiptController(EmpService service)
        {
            this._emp = service;
        }

        public IActionResult Index(string empId)
        {
            if(string.IsNullOrEmpty(empId))
            {
                return this.View("Error", new ErrorViewModel { RequestId = empId });
            }

            var datas =  this._emp.GetSalesReceipts(empId).Select(i=> SalesReceiptMapper.ToViewModel(i));

            var pageModel = new SalesReceiptQueryPage(empId, datas.ToList());
            return this.View(pageModel);
        }

        public IActionResult Create(string empId)
        {
            if (string.IsNullOrEmpty(empId))
            {
                return this.View("Error", new ErrorViewModel { RequestId = empId });
            }

            return this.View(new SalesReceiptAddViewModel { EmpId = empId });
        }
    }
}
