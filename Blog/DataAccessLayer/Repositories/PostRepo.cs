using System.Collections.Generic;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class PostRepo : IPostRepo
    {
        private readonly ApplicationDbContext _context;

        public PostRepo()
        {
            _context = new ApplicationDbContext();
        }

        public IList<Post> GetAll()
        {
            return _context.Posts
                .Include(post => post.Comments)
                .ToList();
        }

        public Post GetById(int id)
        {
            return _context.Posts
                .Include(post => post.Comments)
                .First(post => post.Id == id);
        }
    }
}
