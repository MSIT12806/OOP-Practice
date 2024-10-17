using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Adapter;
using PaymentSystem.Application.Emp;

namespace PaymentSystem.Controllers
{
    public class GuildController : Controller
    {
        private ServiceChargeMapper _mapper;
        private ServiceChargeService _service;

        public GuildController(ServiceChargeService service, ServiceChargeMapper mapper)
        {
            this._mapper = mapper;
            this._service = service;
        }
        public IActionResult SetServiceCharge()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetServiceCharge(ServiceChargeViewModel viewModel)
        {
            _service.SetServiceCharge(_mapper.ToCoreModel(viewModel));
            return RedirectToAction("Index", "Home");
        }
    }
}
