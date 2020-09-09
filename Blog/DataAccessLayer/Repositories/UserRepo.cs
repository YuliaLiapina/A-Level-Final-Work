using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories
{
    public class UserRepo : IUserRepo
    {
        public IList<ApplicationUser>Get()
        {
            using (var context = new ApplicationDbContext())
            {
                var users = context
                    .Users
                    .Include(user => user.Awards)
                    .ToList();

                return users;
            }
        }

        public ApplicationUser Get(string userId)
        {
            using(var context = new ApplicationDbContext())
            {
                var user = context
                    .Users
                    .Include(_user => _user.Awards)
                    .FirstOrDefault(_user => _user.Id == userId);

                return user;
            }
        }

        public void Update (ApplicationUser user)
        {
            using (var context = new ApplicationDbContext())
            {
                var editUser = context
                    .Users
                    .Include(_user => _user.Awards)
                    .FirstOrDefault(_user => _user.Id == user.Id);

                if (editUser == null)
                {
                    context.Users.Add(editUser);
                }
                else
                {
                    foreach (var award in user.Awards)
                    {
                        var selectedAward = context.Awards.FirstOrDefault(_award => _award.Id == award.Id);
                        if (selectedAward != null && editUser.Awards.All(_award => _award.Id != selectedAward.Id))
                        {
                            editUser.Awards.Add(selectedAward);
                        }
                    }

                    editUser.FullName = user.FullName;
                    editUser.IsUserBlocked = user.IsUserBlocked;
                    editUser.CommentsWriteCount = user.CommentsWriteCount;
                    editUser.PostsReadCount = user.PostsReadCount;
                    editUser.PostsWriteCount = user.PostsWriteCount;
                    editUser.CommentsWriteCount = user.CommentsWriteCount;

                    context.Users.Attach(editUser);
                    context.Entry(editUser).State = EntityState.Modified;
                }

                context.SaveChanges();
            }
        }
    }
}
