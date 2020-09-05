using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Models;
using System.Data.Entity;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories
{
    public class PostRepo : RepositoryBase<Post, int>
    {
        public PostRepo(ApplicationDbContext context) : base(context)
        {
        }

        public override Post Get(int id)
        {
            return _dbSet
                .Include(article => article.Comments)
                .Include(article => article.Author)
                .FirstOrDefault(x => x.Id == id);
        }

        public override IEnumerable<Post> Get()
        {
            return _dbSet
                .Include(article => article.Author)
                .ToList();
        }
    }
}
