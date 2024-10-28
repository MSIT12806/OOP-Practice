using LH.Tool.Decoupling;
using Microsoft.AspNetCore.Mvc;
using Payment.Application;

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
            var result = protectedPaymentService.Pay(DateProvider.Now);
            return View(result);
        }
    }
}
