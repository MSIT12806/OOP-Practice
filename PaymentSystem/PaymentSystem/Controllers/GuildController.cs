using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Adapter.Payment;
using PaymentSystem.Application;
using PaymentSystem.Models.Payment;

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
            emp.SubmitServiceCharge(viewModel.Amount,viewModel.ChargeDate);
            return this.RedirectToAction("Index", "Home");
        }
    }
}
