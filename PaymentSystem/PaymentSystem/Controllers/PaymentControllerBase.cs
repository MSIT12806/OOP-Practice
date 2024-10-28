using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Application;

namespace PaymentSystem.Controllers
{
    public class PaymentControllerBase : Controller
    {
        protected PaymentService protectedPaymentService;

        public PaymentControllerBase(PaymentService service)
        {
            this.protectedPaymentService = service;
        }
    }
}
