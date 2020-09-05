using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories
{
    public class CommentRepo : RepositoryBase<Comment, int>
    {
        public CommentRepo(ApplicationDbContext context) : base(context)
        {
        }

        public override IEnumerable<Comment> Get()
        {
            return _dbSet
                .Include(article => article.Author)
                .ToList();
        }
    }
}
