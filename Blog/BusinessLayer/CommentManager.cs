using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace BusinessLayer
{
    public class CommentManager : ICommentManager
    {
        private readonly IRepository<Comment, int> _commentRepo;
        private readonly IMapper _mapper;

        public CommentManager(IRepository<Comment, int> commentRepository, IMapper mapper)
        {
            _commentRepo = commentRepository;
            _mapper = mapper;
        }

        public void Add(CommentModel comment)
        {
            _commentRepo.Add(_mapper.Map<Comment>(comment), true);
        }
    }
}
