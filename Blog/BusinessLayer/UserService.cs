using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessLayer.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNet.Identity;

namespace BusinessLayer.Models
{
    public class UserService : IUserService
    {
        private const int MAX_COMMENTS_WRITE = 10;
        private const int MAX_POST_READ = 10;

        private readonly IRepository<Awards, int> _awardsRepo;
        private readonly IUserRepo _userRepository;
        private readonly IUserPostManager _userPostManager;
        private readonly IMapper _mapper;

        public UserService(IUserRepo userRepository, IUserPostManager userPostManager, IMapper mapper, IRepository<Awards, int> awardsRepository)
        {
            _awardsRepo = awardsRepository;
            _userPostManager = userPostManager;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public void Update(ApplicationUserModel user)
        {
            var userModel = _mapper.Map<ApplicationUser>(user);
            _userRepository.Update(userModel);
        }

        public IList<ApplicationUserModel> Get()
        {
            var users = _userRepository.Get();
            var usersModel = _mapper.Map<IList<ApplicationUserModel>>(users);

            return usersModel;
        }

        public ApplicationUserModel Get(string id)
        {
            var user = _userRepository.Get(id);

            var userModel = _mapper.Map<ApplicationUserModel>(user);
            return userModel;
        }

        public void UserAddPost(string userId)
        {
            var user = Get(userId);

            user.PostsWriteCount++;
            Update(user);
        }

        public void UserAddComment(string userId)
        {
            var user = Get(userId);

            user.CommentsWriteCount++;
            Update(user);
        }

        public void UserViewPost(UserManager<ApplicationUser> userManager, string userId, PostModel _post)
        {
            var user = userManager.FindById(userId);
            if (user != null && _post != null && !_userPostManager.IsViewPost(_post, user.Id))
            {
                user.PostsReadCount++;
                userManager.Update(user);
                CheckUserStatus(userManager, user.Id);
                CheckUserAwards(user.Id);
            }
        }

        public void CheckUserStatus(UserManager<ApplicationUser> userManager, string userId)
        {
            var user = userManager.FindById(userId);
            if (userManager.IsInRole(user.Id, "author") && user.CommentsWriteCount >= MAX_COMMENTS_WRITE && user.PostsReadCount >= MAX_POST_READ)
            {
                userManager.AddToRole(user.Id, "author");
                userManager.Update(user);
            }
        }

        public void CheckUserAwards(string userId)
        {
            var user = Get(userId);
            var awards = _mapper.Map<IList<AwardsModel>>(_awardsRepo.Get());

            foreach (var award in awards.ToList())
            {
                if (!user.Awards.Select(x => x.Id).Contains(award.Id) && user.CommentsWriteCount >= award.CommentWriteCount &&
                    user.PostsWriteCount >= award.PostWriteCount && user.PostsReadCount >= award.PostReadCount)
                {
                    user.Awards.Add(award);
                    Update(user);
                }
            }
        }
    }
}
