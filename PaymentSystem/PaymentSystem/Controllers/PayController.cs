using Microsoft.AspNetCore.Mvc;

namespace PaymentSystem.Controllers
{
    public class PayController : Controller
    {
        public IActionResult Payday()
        {
            return View();
        }
    }
}
