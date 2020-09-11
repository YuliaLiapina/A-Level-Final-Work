using System;
using System.Collections.Generic;
using System.Linq;
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
    public class PostController : Controller
    {
        private readonly IPostManager _postManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private const int PAGE_SIZE = 6;
        private const int MOST_VIEWED = 3;

        public PostController(IPostManager postManager, IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _postManager = postManager;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public ActionResult Index(int page = 0, string authorId = null)
        {
            var authors = GetUsersByRoleName("author");

            var entities = authorId != null
                ? _postManager.Get(x => !x.IsBlocked && x.AuthorId == authorId).Reverse()
                : _postManager.Get(x => !x.IsBlocked);

            var mostViewedData = entities.OrderByDescending(x => x.UsersReadCount).Take(MOST_VIEWED).ToList();
            var mostViewed = _mapper.Map<IList<PostViewModel>>(mostViewedData);

            var count = entities.Count();

            var data = entities.Reverse().Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToList();
            var entitiesMap = _mapper.Map<IList<PostViewModel>>(data);
            var result = new AllPostsViewModel
            {
                Posts = entitiesMap,
                MostViewed = mostViewed,
                Authors = authors,
                Page = page,
                MaxPage = (count / PAGE_SIZE) - (count % PAGE_SIZE == 0 ? 1 : 0),
                AuthorId = authorId
            };

            return View(result);
        }

        [Authorize(Roles="author")]
        public ActionResult MyPosts(int page = 0)
        {
            var userId = User.Identity.GetUserId();
            var entities = _postManager.Get(x => x.AuthorId == userId).Reverse();
            var count = entities.Count();
            var data = entities.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToList();
            var entitiesMap = _mapper.Map<IList<PostViewModel>>(data);
            var user = _userService.Get(User.Identity.GetUserId());
            var result = new AllPostsViewModel
            {
                Posts = entitiesMap,
                Page = page,
                MaxPage = (count / PAGE_SIZE) - (count % PAGE_SIZE == 0 ? 1 : 0),
                IsUserBlocked = user?.IsUserBlocked ?? false
            };

            return View(result);
        }

        [Authorize(Roles="admin")]
        public ActionResult AllPosts(int page = 0)
        {
            var entities = _postManager.Get().Reverse();
            var count = entities.Count();
            var data = entities.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToList();
            var entitiesMap = _mapper.Map<IList<PostViewModel>>(data);
            var result = new AllPostsViewModel
            {
                Posts = entitiesMap,
                Page = page,
                MaxPage = (count / PAGE_SIZE) - (count % PAGE_SIZE == 0 ? 1 : 0)
            };

            return View(result);
        }

        [AllowAnonymous]
        public ActionResult Search(string searchString, string ReturnUrl)
        {
            if (searchString != null && !string.IsNullOrEmpty(searchString.Trim()))
            {
                var entity = _postManager.Get(x => !x.IsBlocked && x.Title.ToLower().Contains(searchString.ToLower()));
                var entitiesMap = _mapper.Map<IList<PostViewModel>>(entity);

                var result = new SearchViewModel
                {
                    Posts = entitiesMap,
                    SearchString = searchString
                };

                return View(result);
            }

            if (searchString == null)
            {
                var result = new SearchViewModel
                {
                    Posts = new List<PostViewModel>(),
                    SearchString = ""
                };
                return View(result);
            }

            return Redirect(ReturnUrl);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var entity = _postManager.Get(id);
            if (Request.IsAuthenticated)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                _userService.UserViewPost(userManager, User.Identity.GetUserId(), entity);
            }
            var user = _userService.Get(User.Identity.GetUserId());
            var result = new PostDetailsViewModel
            {
                Post = _mapper.Map<PostViewModel>(entity),
                IsUserBlocked = user?.IsUserBlocked ?? false,
                UserPostsReadCount = user?.PostsReadCount ?? 0,
                UserCommentsWriteCount = user?.CommentsWriteCount ?? 0
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
        public ActionResult Create(CreatePostViewModel _post)
        {
            if (ModelState.IsValid)
            {
                var _newPost = _mapper.Map<PostModel>(_post);
                _newPost.PublishDate = DateTime.Now;
                _newPost.IsBlocked = false;
                _newPost.AuthorId = User.Identity.GetUserId();
                _newPost.UsersReadCount = 0;

                _postManager.Add(_newPost);
                _userService.UserAddPost(User.Identity.GetUserId());

                return RedirectToAction("MyPosts");
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
            var entity = _postManager.Get(id);
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

        public List<ApplicationUserModel> GetUsersByRoleName(string roleName)
        {
            List<ApplicationUserModel> users = new List<ApplicationUserModel>();
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            foreach (var user in _userService.Get())
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
