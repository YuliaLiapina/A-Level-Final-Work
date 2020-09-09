using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Blog.Models;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;

namespace Blog.Controllers
{
    [Authorize(Roles="admin")]
    public class AwardController : Controller
    {
        private readonly IAwardManager _awardManager;
        private readonly IMapper _mapper;

        public AwardController(IAwardManager awardManager, IUserService userService, IMapper mapper)
        {
            _awardManager = awardManager;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            var awards = _mapper.Map<List<AwardsViewModel>>(_awardManager.Get());

            return View(awards);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var entity = _awardManager.Get(id);
            var result = _mapper.Map<AwardEditViewModel>(entity);

            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(AwardEditViewModel _award)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<AwardsModel>(_awardManager.Get(_award.Id));
                entity.Name = _award.Name;
                _awardManager.Update(entity);

                return RedirectToAction("Index");
            }

            return View(_award);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateAwardViewModel _award)
        {
            if (ModelState.IsValid)
            {
                var _newAward = _mapper.Map<AwardsModel>(_award);
                _awardManager.Add(_newAward);

                return RedirectToAction("Index");
            }

            return View(_award);
        }
    }
}
