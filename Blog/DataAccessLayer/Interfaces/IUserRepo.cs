using System.Collections.Generic;

namespace DataAccessLayer.Interfaces
{
    public interface IUserRepo
    {
        IList<ApplicationUser> Get();
        ApplicationUser Get(string userId);
        void Update(ApplicationUser user);
    }
}
