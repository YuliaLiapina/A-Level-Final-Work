using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Awards = new List<Awards>();
        }

        public bool IsUserBlocked { get; set; }
        public int PostsReadCount { get; set; }
        public int PostsWriteCount { get; set; }
        public int CommentsWriteCount { get; set; }
        public string FullName { get; set; }

        public ICollection<Awards> Awards { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}
