using System.Collections.Generic;
using System.Linq;
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
        private readonly IMapper _mapper;

        public PostManager(IRepository<Post, int> postRepository, IMapper mapper)
        {
            _postRepo = postRepository;
            _mapper = mapper;
        }

        public IList<PostModel> GetAll()
        {
            var entities = _postRepo.Get();
            var entityModels = _mapper.Map<IList<PostModel>>(entities);

            return entityModels;
        }

        public PostModel GetById(int id)
        {
            var entity = _postRepo.Get(id);
            var entityModel = _mapper.Map<PostModel>(entity);

            return entityModel;
        }

        public void Add(PostModel post)
        {
            _postRepo.Add(_mapper.Map<Post>(post), true);
        }
    }
}
