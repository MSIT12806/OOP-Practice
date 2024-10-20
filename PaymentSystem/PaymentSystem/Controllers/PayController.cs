using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Application.Payday;

namespace PaymentSystem.Controllers
{
    public class PayController : PayControllerBase
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
            var result = protectedPaydayService.Pay();
            return View(result);
        }
    }
}
