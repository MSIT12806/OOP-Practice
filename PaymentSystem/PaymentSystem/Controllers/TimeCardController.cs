using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Application;
using PaymentSystem.Models.Payment;
using PaymentSystem.ViewModel;

namespace PaymentSystem.Controllers
{
    public class TimeCardController : PaymentControllerBase
    {
        public TimeCardController(PaymentService service) : base(service)
        {
        }

        public IActionResult Index(string empId)
        {
            if (string.IsNullOrEmpty(empId))
            {
                return this.View("Error", new ErrorViewModel { RequestId = empId });
            }

            var emp = protectedPaymentService.Rebuild(empId) as HourlyEmployee;
            return View(emp.TimeCards);
        }
    }
}
