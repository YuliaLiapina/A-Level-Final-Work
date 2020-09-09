using System;
using System.Web;
using System.Web.Mvc;
using Blog.Models;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Blog.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentManager _commentManager;
        private readonly IUserService _userService;

        public CommentController(IUserService userService, ICommentManager commentManager)
        {
            _userService = userService;
            _commentManager = commentManager;
        }

        [HttpPost]
        [Authorize(Roles="user")]
        public RedirectToRouteResult AddComment(AddCommentViewModel comment)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var user = _userService.Get(User.Identity.GetUserId());

                var _newComment = new CommentModel()
                {
                    PublishDate = DateTime.Now,
                    Body = comment.Body,
                    IsBlocked = false,
                    AuthorId = User.Identity.GetUserId(),
                    PostId = comment.PostId
                };

                _userService.UserAddComment(user.Id);
                _userService.CheckUserStatus(userManager, user.Id);
                _userService.CheckUserAwards(user.Id);

                _commentManager.Add(_newComment);
            }

            return RedirectToAction("Details", "Post", new { id = comment.PostId });
        }
    }
}
