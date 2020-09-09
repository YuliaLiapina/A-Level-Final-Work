using System.Collections.Generic;
using BusinessLayer.Models;
using DataAccessLayer;
using Microsoft.AspNet.Identity;

namespace BusinessLayer.Interfaces
{
    public interface IUserService
    {
        void Update(ApplicationUserModel user);
        IList<ApplicationUserModel> Get();
        ApplicationUserModel Get(string id);
        void UserAddPost(string userId);
        void UserAddComment(string userId);
        void CheckUserStatus(UserManager<ApplicationUser> userManager, string userId);
        void CheckUserAwards(string userId);
        void UserViewPost(UserManager<ApplicationUser> userManager, string userId, PostModel _post);
    }
}
