using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Blog.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Blog.Controllers
{
    public class UserController : Controller
    {
        [Authorize(Roles="admin")]
        public ActionResult Index()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var users = userManager.Users.OrderBy(a => a.FullName).ToList();

            return View(users);
        }

        [HttpGet]
        [Authorize(Roles="user")]
        public async Task<ActionResult> EditMyAccount()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());

            var result = new EditApplicationUser
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                IsUserBlocked = user.IsUserBlocked,
                ReturnUrl = Url.Action("Index", "Manage")
            };

            return View("Edit", result);
        }

        [HttpGet]
        [Authorize(Roles="admin")]
        public async Task<ActionResult> Edit(string id)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = await userManager.FindByIdAsync(id);

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
        public async Task<ActionResult> Edit(EditApplicationUser _user)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var user = await userManager.FindByIdAsync(_user.Id);

                user.FullName = _user.FullName;
                user.Email = _user.Email;
                user.IsUserBlocked = _user.IsUserBlocked;

                await userManager.UpdateAsync(user);

                return Redirect(_user.ReturnUrl);
            }

            return View(_user);
        }
    }
}
