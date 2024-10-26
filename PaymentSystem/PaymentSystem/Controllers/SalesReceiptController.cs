using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Adapter;
using PaymentSystem.Application;
using PaymentSystem.Models.Payment;
using PaymentSystem.ViewModel;

namespace PaymentSystem.Controllers
{
    public class SalesReceiptController : Controller
    {
        private PaymentService _service;

        public SalesReceiptController(PaymentService service)
        {
            this._service = service;
        }

        public IActionResult Index(string empId)
        {
            if(string.IsNullOrEmpty(empId))
            {
                return this.View("Error", new ErrorViewModel { RequestId = empId });
            }

            var datas =  (this._service.Rebuild(empId) as SalesEmployee).GetSalesReceipts().Select(i=> SalesReceiptMapper.ToViewModel(i));

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
