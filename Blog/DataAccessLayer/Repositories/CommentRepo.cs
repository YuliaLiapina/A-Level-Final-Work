using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories
{
    public class CommentRepo : RepositoryBase<Post, int>
    {
        public CommentRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
