using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories
{
    public class PostRepo : RepositoryBase<Post, int>
    {
        public PostRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
