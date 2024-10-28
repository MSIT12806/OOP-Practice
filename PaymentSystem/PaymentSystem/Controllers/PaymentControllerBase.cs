using Microsoft.AspNetCore.Mvc;
using Payment.Application;

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
