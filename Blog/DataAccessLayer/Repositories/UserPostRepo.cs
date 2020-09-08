using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories
{
    public class UserPostRepo : RepositoryBase<UserPost, int>
    {
        public UserPostRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
