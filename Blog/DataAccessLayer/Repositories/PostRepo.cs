using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Models;
using System.Data.Entity;
using System.Linq.Expressions;

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
                .Include(article => article.Comments.Select(x => x.Author))
                .Include(article => article.Author)
                .FirstOrDefault(x => x.Id == id);
        }

        public override IEnumerable<Post> Get()
        {
            return _dbSet
                .Include(article => article.Author)
                .ToList();
        }

        public override IEnumerable<Post> Get(Expression<Func<Post, bool>> expression)
        {
            return _dbSet
                .Include(article => article.Comments)
                .Include(article => article.Author)
                .Where(expression)
                .ToList();
        }
    }
}
