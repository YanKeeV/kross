using SampleMvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SampleMvcApp.Controllers
{
    public class AuthController : Controller
    {
        private AuthService _authService;

        public AuthController(AuthService authService) => _authService = authService;

        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Login(UserModel model)
        {
            var isAuthenticated = _authService.Login(model);

            if (isAuthenticated)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError(string.Empty, "Invalid login attempt");

            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserModel model)
        {
            var result = _authService.Register(model);

            if (result)
                return RedirectToAction("Index", "Home");

            return View(model);
        }
    }
}
