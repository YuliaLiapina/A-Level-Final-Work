using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Blog.Models;
using BusinessLayer.Interfaces;

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
            var result = _mapper.Map<IList<PostViewModel>>(entities);

            return View();
        }
    }
}
