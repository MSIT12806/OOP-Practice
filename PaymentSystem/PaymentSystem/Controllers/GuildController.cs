using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Adapter;
using PaymentSystem.Application.Emp;

namespace PaymentSystem.Controllers
{
    public class GuildController : Controller
    {
        private ServiceChargeService _service;

        public GuildController(ServiceChargeService service)
        {
            this._service = service;
        }
        public IActionResult AddServiceCharge()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddServiceCharge(ServiceChargeViewModel viewModel)
        {
            this._service.AddServiceCharge(viewModel.EmpId, viewModel.Amount, viewModel.ChargeDate);
            return this.RedirectToAction("Index", "Home");
        }
    }
}
