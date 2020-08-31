using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories
{
    public class CommentRepo : ICommentRepo
    {
        private readonly ApplicationDbContext _context;

        public CommentRepo()
        {
            _context = new ApplicationDbContext();
        }

        public IList<Comment> GetAll()
        {
            return _context.Comments
                .Include(comment => comment.Post)
                .ToList();
        }
    }
}
