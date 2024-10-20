using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Application.Payday;

namespace PaymentSystem.Controllers
{
    public class PayControllerBase : Controller
    {
        protected PaydayService protectedPaydayService;

        public PayControllerBase(PaydayService service)
        {
            this.protectedPaydayService = service;
        }
    }
}
