using IdentityModule.Implement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Adapter.IdentityValidation;

namespace PaymentSystem.Controllers.Identity
{
    public class AuthController : Controller
    {
        SignInManager<AspNetUser> signInManager;
        public AuthController(SignInManager<AspNetUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel vm)
        {
            var result = this.signInManager.PasswordSignInAsync(vm.UserName, vm.Password, true, false).Result;
            if (result.Succeeded)
            {
                return this.RedirectToAction("Index", "Home");
            }
            else
            {
                return this.RedirectToAction("Login");
            }
        }
    }

}
