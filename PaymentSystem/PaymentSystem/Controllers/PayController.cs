using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Application.Payday;

namespace PaymentSystem.Controllers
{
    public class PayController : Controller
    {
        private PaydayService _service;

        public PayController(PaydayService service)
        {
            this._service = service;
        }

        public IActionResult Payday()
        {
            return RedirectToAction(nameof(PayController.PayResult), nameof(PayController).Replace("Controller", ""));
        }

        public IActionResult PayResult()
        {
            var result = _service.Pay();
            return View(result);
        }
    }
}
