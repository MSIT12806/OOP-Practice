using Microsoft.AspNetCore.Mvc;
using Payment.Application;
using Payment.Models.Payment;
using PaymentSystem.Adapter.Payment;

namespace PaymentSystem.Controllers
{
    public class GuildController : PaymentControllerBase
    {
        public GuildController(PaymentService service) : base(service)
        {
        }

        public IActionResult AddServiceCharge()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddServiceCharge(ServiceChargeViewModel viewModel)
        {
            var emp = this.protectedPaymentService.Rebuild(viewModel.EmpId) as UnionEmployee;
            emp.SubmitServiceCharge(viewModel.Amount,viewModel.ChargeDate.ToDateTime(TimeOnly.MinValue));
            return this.RedirectToAction("Index", "Home");
        }
    }
}
