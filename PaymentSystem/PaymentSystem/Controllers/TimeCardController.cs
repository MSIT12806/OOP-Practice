using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Adapter.Payday;
using PaymentSystem.Application.Payday;
using PaymentSystem.ViewModel;

namespace PaymentSystem.Controllers
{
    public class TimeCardController : PayControllerBase
    {
        public TimeCardController(PaydayService service) : base(service)
        {
        }

        public IActionResult Index(string empId)
        {
            if (string.IsNullOrEmpty(empId))
            {
                return this.View("Error", new ErrorViewModel { RequestId = empId });
            }

            var timeCards = protectedPaydayService.GetTimeCards(empId);
            return View(timeCards);
        }

        public IActionResult SaveTimeCard(string timeCardId)
        {
            var saveVM = protectedPaydayService.GetTimeCard(timeCardId);
            return View(saveVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveTimeCard(TimeCardSaveViewModel vm)
        {
            return RedirectToAction(nameof(TimeCardController.Index), new { empId = vm.EmpId });
        }
    }
}
