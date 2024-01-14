using Domains.Domains;
using Domains.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Repositories;

namespace Web.Controllers {
    public class PasswordInfoController : Controller {
        private readonly PasswordInfoRepository _passwordInfoRepository;
        private readonly UserRepository _userRepository;

        public PasswordInfoController(PasswordInfoRepository passwordInfoRepository, UserRepository userRepository) {
            _passwordInfoRepository = passwordInfoRepository;
            _userRepository = userRepository;
        }

        public IActionResult Index() {
            var currentUser = _userRepository.GetByLogin(User.Identity.Name);
            var passwordInfos = _passwordInfoRepository.GetPasswordInfosByUser(currentUser);
            var model = new PasswordListModel() {
                PasswordInfos = passwordInfos,
                ConfirmCode = currentUser.ConfirmCode
            };
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
    }
}
