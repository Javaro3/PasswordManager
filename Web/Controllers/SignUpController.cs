using Domains.Domains;
using Domains.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Repository.Repositories;
using System.Security.Claims;

namespace Web.Controllers {
    public class SignUpController : Controller {
        private readonly UserRepository _userRepository;

        public SignUpController(UserRepository userRepository) {
            _userRepository = userRepository;
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult SignUp(RegisterModel model) {
            if (model.Password != model.RepeatPassword) {
                ModelState.AddModelError(string.Empty, "Password is not equal repeat password");
            }
            if (_userRepository.IsLoginExist(model.Login)) {
                ModelState.AddModelError(string.Empty, $"{model.Login} is exist");
            }
            else if (ModelState.IsValid) {
                var claims = new List<Claim>{
                    new Claim(ClaimTypes.Name, model.Login),
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                var newUser = new User() {
                    Login = model.Login,
                    Password = model.Password,
                    ConfirmCode = model.ConfirmCode
                };

                _userRepository.Add(newUser);
                return RedirectToAction("Index", "PasswordInfo");
            }

            return View("Index", model);
        }

        public IActionResult Logout() {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "PasswordInfo");
        }
    }
}
