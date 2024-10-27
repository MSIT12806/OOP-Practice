using Microsoft.AspNetCore.Mvc;

namespace PaymentSystem.Controllers
{
    public class PayController : PaymentControllerBase
    {
        public PayController(PaydayService service) : base(service)
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
