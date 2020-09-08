using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Blog.Models;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using DataAccessLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Blog.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostManager _postManager;
        private readonly IUserPostManager _userPostManager;
        private readonly IMapper _mapper;
        private const int _pageSize = 6;

        public PostController(IPostManager postManager, IUserPostManager userPostManager, IMapper mapper)
        {
            _postManager = postManager;
            _userPostManager = userPostManager;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public ActionResult Index(int page = 0, string authorId = null)
        {
            var authors = GetUsersByRoleName("author");

            var entities = authorId != null
                ? _postManager.GetAll(x => !x.IsBlocked && x.AuthorId == authorId).Reverse()
                : _postManager.GetAll(x => !x.IsBlocked).Reverse();

            var count = entities.Count();

            var data = entities.Skip(page * _pageSize).Take(_pageSize).ToList();
            var entitiesMap = _mapper.Map<IList<PostViewModel>>(data);
            var result = new AllPostsViewModel
            {
                Posts = entitiesMap,
                Authors = authors,
                Page = page,
                MaxPage = (count / _pageSize) - (count % _pageSize == 0 ? 1 : 0),
                AuthorId = authorId
            };

            return View(result);
        }

        [Authorize(Roles="author")]
        public ActionResult MyPosts(int page = 0)
        {
            var entities = _postManager.GetByAuthorId(User.Identity.GetUserId()).Reverse();
            var count = entities.Count();
            var data = entities.Skip(page * _pageSize).Take(_pageSize).ToList();
            var entitiesMap = _mapper.Map<IList<PostViewModel>>(data);
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.FindById(User.Identity.GetUserId());
            var result = new AllPostsViewModel
            {
                Posts = entitiesMap,
                Page = page,
                MaxPage = (count / _pageSize) - (count % _pageSize == 0 ? 1 : 0),
                IsUserBlocked = user?.IsUserBlocked ?? false
            };

            return View(result);
        }

        [Authorize(Roles="author")]
        public ActionResult AllPosts(int page = 0)
        {
            var entities = _postManager.GetAll().Reverse();
            var count = entities.Count();
            var data = entities.Skip(page * _pageSize).Take(_pageSize).ToList();
            var entitiesMap = _mapper.Map<IList<PostViewModel>>(data);
            var result = new AllPostsViewModel
            {
                Posts = entitiesMap,
                Page = page,
                MaxPage = (count / _pageSize) - (count % _pageSize == 0 ? 1 : 0)
            };

            return View(result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Details(int id)
        {
            var entity = _postManager.GetById(id);
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null && entity != null && !_userPostManager.IsViewPost(entity, user))
            {
                user.PostsReadCount++;
                await userManager.UpdateAsync(user);
            }
            var result = new PostDetailsViewModel
            {
                Post = _mapper.Map<PostViewModel>(entity),
                IsUserBlocked = user?.IsUserBlocked ?? false
            };

            return View(result);
        }

        [HttpGet]
        [Authorize(Roles="author")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles="author")]
        public async Task<ActionResult> Create(CreatePostViewModel _post)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
                var _newPost = _mapper.Map<PostModel>(_post);
                _newPost.PublishDate = DateTime.Now;
                _newPost.IsBlocked = false;
                _newPost.AuthorId = User.Identity.GetUserId();
                _newPost.UsersReadCount = 0;

                _postManager.Add(_newPost);

                user.PostsWriteCount++;
                await userManager.UpdateAsync(user);

                return RedirectToAction("Index");
            }

            return View(_post);
        }

        [Authorize(Roles="author")]
        public ActionResult Delete(int id)
        {
            _postManager.RemoveById(id);
            return RedirectToAction("MyPosts");
        }

        [HttpGet]
        [Authorize(Roles="author")]
        public ActionResult Edit(int id, string ReturnUrl)
        {
            var entity = _postManager.GetById(id);
            var result = _mapper.Map<EditPostViewModel>(entity);
            result.ReturnUrl = ReturnUrl;

            return View(result);
        }

        [HttpPost]
        [Authorize(Roles="author")]
        public ActionResult Edit(EditPostViewModel _post)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<PostModel>(_post);
                _postManager.Update(entity);

                return Redirect(_post.ReturnUrl);
            }

            return View(_post);
        }

        public List<ApplicationUser> GetUsersByRoleName(string roleName)
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            foreach (var user in userManager.Users.ToList())
            {
                if (userManager.IsInRole(user.Id, roleName))
                {
                    users.Add(user);
                }
            }

            return users;
        }
    }
}
