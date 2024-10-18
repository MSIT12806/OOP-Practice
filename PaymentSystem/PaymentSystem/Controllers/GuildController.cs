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
        public IActionResult SetServiceCharge()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetServiceCharge(ServiceChargeViewModel viewModel)
        {
            this._service.SetServiceCharge(ServiceChargeMapper.ToCoreModel(viewModel));
            return this.RedirectToAction("Index", "Home");
        }
    }
}
