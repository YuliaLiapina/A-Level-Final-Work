using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Models;

namespace BusinessLayer.Interfaces
{
    public interface IUserPostManager
    {
        IList<UserPostModel> GetAll(Expression<Func<UserPost, bool>> expression);
        void Add(UserPostModel userPost);
        bool IsViewPost(PostModel post, ApplicationUser user);
    }
}
