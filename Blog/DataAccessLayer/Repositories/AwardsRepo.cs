using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories
{
    public class AwardsRepo : RepositoryBase<Awards, int>
    {
        public AwardsRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
