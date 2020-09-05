using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web.Mvc;
using AutoMapper;
using Blog.Models;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using Microsoft.AspNet.Identity;

namespace Blog.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostManager _postManager;
        private readonly IMapper _mapper;

        public PostController(IPostManager postManager, IMapper mapper)
        {
            _postManager = postManager;
            _mapper = mapper;
        }

        // GET
        public ActionResult Index()
        {
            var entities = _postManager.GetAll();
            var entitiesMap = _mapper.Map<IList<PostViewModel>>(entities);
            var result = new AllPostsViewModel { Posts = entitiesMap };

            return View(result);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var entity = _postManager.GetById(id);
            var result = _mapper.Map<PostViewModel>(entity);

            return View(result);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult AddPost(CreatePostViewModel _post)
        {
            var _newComment = new PostModel()
            {
                Title = _post.Title,
                PublishDate = DateTime.Now,
                Body = _post.Body,
                IsBlocked = false,
                IsShowComment = _post.IsShowComment,
                Image = _post.Image,
                Discription = _post.Discription,
                AuthorId = User.Identity.GetUserId()
            };

            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var age = identity.Claims.Where(c => c.Type == "FullName").Select(c => c.Value).SingleOrDefault();


            _postManager.Add(_newComment);

            return RedirectToAction("Index");
        }
    }
}
