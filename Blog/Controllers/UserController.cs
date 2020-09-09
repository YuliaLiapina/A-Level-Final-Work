using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Blog.Models;
using BusinessLayer.Interfaces;
using Microsoft.AspNet.Identity;

namespace Blog.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [Authorize(Roles="admin")]
        public ActionResult Index()
        {
            var users = _mapper.Map<List<ApplicationUserViewModel>>(_userService.Get());

            return View(users);
        }

        [HttpGet]
        [Authorize(Roles="user")]
        public ActionResult MyAccount()
        {
            var user = _userService.Get(User.Identity.GetUserId());

            var result = new MyAccountViewModel()
            {
                User = _mapper.Map<ApplicationUserViewModel>(user)
            };

            return View(result);
        }

        [HttpGet]
        [Authorize(Roles="user")]
        public ActionResult EditMyAccount()
        {
            var user = _userService.Get(User.Identity.GetUserId());

            var result = new EditApplicationUser
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                IsUserBlocked = user.IsUserBlocked,
                ReturnUrl = Url.Action("MyAccount", "User")
            };

            return View("Edit", result);
        }

        [HttpGet]
        [Authorize(Roles="admin")]
        public ActionResult Edit(string userId)
        {
            var user = _userService.Get(userId);

            var result = new EditApplicationUser
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                IsUserBlocked = user.IsUserBlocked,
                ReturnUrl = Url.Action("Index")
            };

            return View(result);
        }

        [HttpPost]
        [Authorize(Roles="user")]
        public ActionResult Edit(EditApplicationUser _user)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.Get(_user.Id);

                user.FullName = _user.FullName;
                user.Email = _user.Email;
                user.IsUserBlocked = _user.IsUserBlocked;

                _userService.Update(user);

                return Redirect(_user.ReturnUrl);
            }

            return View(_user);
        }
    }
}
