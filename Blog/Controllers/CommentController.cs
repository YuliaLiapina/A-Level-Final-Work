using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Blog.Models;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Blog.Controllers
{
    public class CommentController : Controller
    {
        private readonly IPostManager _postManager;
        private readonly ICommentManager _commentManager;
        private readonly IMapper _mapper;

        public CommentController(IPostManager postManager, ICommentManager commentManager, IMapper mapper)
        {
            _postManager = postManager;
            _commentManager = commentManager;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles="user")]
        public async Task<RedirectToRouteResult> AddComment(AddCommentViewModel comment)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var user = await userManager.FindByIdAsync(User.Identity.GetUserId());

                var _newComment = new CommentModel()
                {
                    PublishDate = DateTime.Now,
                    Body = comment.Body,
                    IsBlocked = false,
                    AuthorId = User.Identity.GetUserId(),
                    PostId = comment.PostId
                };

                user.CommentsWriteCount++;
                await userManager.UpdateAsync(user);

                _commentManager.Add(_newComment);
            }

            return RedirectToAction("Details", "Post", new { id = comment.PostId });
        }
    }
}
