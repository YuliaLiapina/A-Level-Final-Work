using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace BusinessLayer
{
    public class UserPostManager : IUserPostManager
    {
        private readonly IRepository<UserPost, int> _userPostRepo;
        private readonly IPostManager _postManager;
        private readonly IMapper _mapper;

        public UserPostManager(IRepository<UserPost, int> userPostRepository, IPostManager postManager, IMapper mapper)
        {
            _userPostRepo = userPostRepository;
            _postManager = postManager;
            _mapper = mapper;
        }

        public IList<UserPostModel> GetAll(Expression<Func<UserPost, bool>> expression)
        {
            var entities = _userPostRepo.Get(expression);
            var entityModels = _mapper.Map<IList<UserPostModel>>(entities);

            return entityModels;
        }

        public void Add(UserPostModel userPost)
        {
            _userPostRepo.Add(_mapper.Map<UserPost>(userPost), true);
        }

        public bool IsViewPost(PostModel post, string userId)
        {
            var entities = GetAll(x => x.PostId == post.Id && x.UserId == userId);
            if (entities.Count == 0)
            {
                Add(new UserPostModel
                {
                    PostId = post.Id,
                    UserId = userId
                });
                post.UsersReadCount++;
                _postManager.Update(post);

                return false;
            }

            return true;
        }
    }
}
