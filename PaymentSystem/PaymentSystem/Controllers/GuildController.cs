using Microsoft.AspNetCore.Mvc;

namespace PaymentSystem.Controllers
{
    public class GuildController : Controller
    {
        public IActionResult ServiceCharge()
        {
            return View();
        }
    }
}
