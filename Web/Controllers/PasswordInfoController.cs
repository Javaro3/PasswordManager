using Domains.Domains;
using Domains.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Repositories;
using Web.Servicies;

namespace Web.Controllers {
    [Authorize]
    public class PasswordInfoController : Controller {
        private readonly PasswordInfoRepository _passwordInfoRepository;
        private readonly UserRepository _userRepository;
        private readonly CookieManager _cookieManager;

        public PasswordInfoController(
            PasswordInfoRepository passwordInfoRepository,
            UserRepository userRepository,
            CookieManager cookieManager) {
            _passwordInfoRepository = passwordInfoRepository;
            _userRepository = userRepository;
            _cookieManager = cookieManager;
        }

        public IActionResult Index() {
            var searchModel = _cookieManager.GetCookie<SearchModel>(HttpContext.Request.Cookies, "search");
            var model = GetPasswordListModel();
            model.SearchModel = searchModel;
            model.PasswordInfos = model.PasswordInfos
                .Where(e => string.IsNullOrEmpty(searchModel.ServiceName) || e.ServiceName.Contains(searchModel.ServiceName));
            
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(SearchModel searchModel) {
            _cookieManager.SetCookie(searchModel, HttpContext.Response.Cookies, "search");
            var model = GetPasswordListModel();
            model.SearchModel = searchModel;
            model.PasswordInfos = model.PasswordInfos
                .Where(e => string.IsNullOrEmpty(searchModel.ServiceName) || e.ServiceName.Contains(searchModel.ServiceName));
            
            return View(model);
        }

        public IActionResult Add() {
            return View();
        }

        [HttpPost]
        public IActionResult Add(PasswordInfoModel model) {
            var currentUser = _userRepository.GetByLogin(User.Identity.Name);
            var newPasswordInfo = new PasswordInfo() {
                ServiceName = model.ServiceName,
                Login = model.Login,
                UserId = currentUser.Id,
                User = currentUser,
                Password = model.Password,
                Description = model.Description
            };
            _passwordInfoRepository.Add(newPasswordInfo);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id) {
            var passwordInfo = _passwordInfoRepository.GetById(id);
            _passwordInfoRepository.Remove(passwordInfo);
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id) {
            var model = _passwordInfoRepository.GetById(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(PasswordInfo model) {
            var currentUser = _userRepository.GetByLogin(User.Identity.Name);
            model.User = currentUser;
            model.UserId = currentUser.Id;
            _passwordInfoRepository.Update(model);
            return RedirectToAction("Index");
        }

        private PasswordListModel GetPasswordListModel() {
            var currentUser = _userRepository.GetByLogin(User.Identity.Name);
            var passwordInfos = _passwordInfoRepository.GetPasswordInfosByUser(currentUser);
            var model = new PasswordListModel() {
                PasswordInfos = passwordInfos,
                ConfirmCode = currentUser.ConfirmCode
            };
            return model;
        }
    }
}
