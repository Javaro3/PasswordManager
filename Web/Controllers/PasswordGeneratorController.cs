using Domains.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Servicies.PasswordGenerator;
using Web.Servicies;

namespace Web.Controllers {
    public class PasswordGeneratorController : Controller {
        private readonly CookieManager _cookieManager;

        public PasswordGeneratorController(CookieManager cookieManager) {
            _cookieManager = cookieManager;
        }

        public IActionResult Index() {
            var model = _cookieManager.GetCookie<PasswordGeneratorModel>(HttpContext.Request.Cookies , "passwordGenerator");
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(PasswordGeneratorModel model) {
            _cookieManager.SetCookie(model, HttpContext.Response.Cookies, "passwordGenerator");

            var generator = new PasswordGenerator(model);
            try {
                generator.Generate();
            }
            catch (Exception e){
                model.Password = e.Message;
            }

            return View(model);
        }
    }
}
