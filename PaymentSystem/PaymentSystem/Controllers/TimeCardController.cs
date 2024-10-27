using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Application;
using PaymentSystem.ViewModel;

namespace PaymentSystem.Controllers
{
    public class TimeCardController : PaymentControllerBase
    {
        public TimeCardController(PaymentService service) : base(service)
        {
        }

        //public IActionResult Index(string empId)
        //{
        //    if (string.IsNullOrEmpty(empId))
        //    {
        //        return this.View("Error", new ErrorViewModel { RequestId = empId });
        //    }

        //    var emp = protectedPaymentService.Rebuild(empId);

        //    var timeCards = protectedPaymentService.GetTimeCards(empId);
        //    return View(timeCards);
        //}

        //public IActionResult SaveTimeCard(string timeCardId)
        //{
        //    var saveVM = protectedPaymentService.GetTimeCard(timeCardId);
        //    return View(saveVM);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult SaveTimeCard(TimeCardSaveViewModel vm)
        //{
        //    return RedirectToAction(nameof(TimeCardController.Index), new { empId = vm.EmpId });
        //}
    }
}
