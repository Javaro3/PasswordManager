using Domains.Domains;
using Domains.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Repository.Repositories;
using System.Security.Claims;

namespace Web.Controllers {
    public class AuthorizeController : Controller {
        private readonly UserRepository _userRepository;

        public AuthorizeController(UserRepository userRepository) {
            _userRepository = userRepository;
        }

        public IActionResult SignUp() {
            return View();
        }

        public IActionResult LogIn() {
            return View();
        }

        [HttpPost]
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
                    AutorizeUser(model.Login);
                    return RedirectToAction("Index", "PasswordInfo");
                }
            }
            return View("Index", model);
        }

        [HttpPost]
        public IActionResult SignUp(RegisterModel model) {
            if (model.Password != model.RepeatPassword) {
                ModelState.AddModelError(string.Empty, "Password is not equal repeat password");
            }
            if (_userRepository.IsLoginExist(model.Login)) {
                ModelState.AddModelError(string.Empty, $"{model.Login} is exist");
            }
            else if (ModelState.IsValid) {
                AutorizeUser(model.Login);

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
            return RedirectToAction("Index", "Home");
        }

        private void AutorizeUser(string login) {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, login) };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}