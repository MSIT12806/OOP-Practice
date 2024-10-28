using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Application;

namespace PaymentSystem.Controllers
{
    public class PayController : PaymentControllerBase
    {
        public PayController(PaymentService service) : base(service)
        {
        }

        public IActionResult Payday()
        {
            return RedirectToAction(nameof(PayController.PayResult), nameof(PayController).Replace("Controller", ""));
        }

        public IActionResult PayResult()
        {
            var result = protectedPaymentService.Pay(DateOnly.FromDateTime(DateTime.Now));
            return View(result);
        }
    }
}
