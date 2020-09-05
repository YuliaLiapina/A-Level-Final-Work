using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Blog.Models;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using DataAccessLayer.Models;
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
        // [Authorize(Roles="admin")]
        public RedirectToRouteResult AddComment(AddCommentViewModel comment)
        {
            // var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            // var user = await userManager.FindByNameAsync(User.Identity.Name);

            var _newComment = new CommentModel()
            {
                PublishDate = DateTime.Now,
                Body = comment.Body,
                IsBlocked = false,
                AuthorId = User.Identity.GetUserId(),
                PostId = comment.PostId
            };

            _commentManager.Add(_newComment);

            return RedirectToAction("Details", "Post", new { id = comment.PostId });
        }
    }
}
