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
    public class PostManager : IPostManager
    {
        private readonly IRepository<Post, int> _postRepo;
        private readonly IRepository<Comment, int> _commentRepo;
        private readonly IMapper _mapper;

        public PostManager(IRepository<Post, int> postRepository, IRepository<Comment, int> commentRepository, IMapper mapper)
        {
            _postRepo = postRepository;
            _commentRepo = commentRepository;
            _mapper = mapper;
        }

        public IList<PostModel> Get()
        {
            var entities = _postRepo.Get();
            var entityModels = _mapper.Map<IList<PostModel>>(entities);

            return entityModels;
        }

        public IList<PostModel> Get(Expression<Func<Post, bool>> expression)
        {
            var entities = _postRepo.Get(expression);
            var entityModels = _mapper.Map<IList<PostModel>>(entities);

            return entityModels;
        }

        public PostModel Get(int id)
        {
            var entity = _postRepo.Get(id);
            var entityModel = _mapper.Map<PostModel>(entity);

            return entityModel;
        }

        public void Add(PostModel post)
        {
            _postRepo.Add(_mapper.Map<Post>(post), true);
        }

        public void RemoveById(int postId)
        {
            var _post = Get(postId);
            foreach (var comment in _post.Comments)
            {
                _commentRepo.DeleteById(comment.Id, false);
            }
            _postRepo.DeleteById(postId, true);
        }

        public void Update(PostModel _post)
        {
            var entity = _postRepo.Get(_post.Id);

            entity.IsBlocked = _post.IsBlocked;
            entity.IsShowComment = _post.IsShowComment;
            entity.Title = _post.Title;
            entity.Image = _post.Image;
            entity.Discription = _post.Discription;
            entity.Body = _post.Body;
            entity.UsersReadCount = _post.UsersReadCount;

            _postRepo.Update(entity, true);
        }
    }
}
