using Domains.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Repository.Repositories;
using System.Security.Claims;

namespace Web.Controllers {
    public class LogInController : Controller {
        private readonly UserRepository _userRepository;

        public LogInController(UserRepository userRepository) {
            _userRepository = userRepository;
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult LogIn(LoginModel model) {
            if (ModelState.IsValid) {
                var user = _userRepository.GetByLogin(model.Login);
                if (user == null) {
                    ModelState.AddModelError(string.Empty, $"{model.Login} is not exist");
                }
                else if (!_userRepository.CheckPassword(user, model.Password)) {
                    ModelState.AddModelError(string.Empty, $"Password is wrong");
                }
                else {
                    var claims = new List<Claim>{
                        new Claim(ClaimTypes.Name, model.Login),
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "PasswordInfo");
                }
            }
            return View("Index", model);
        }
    }
}
