using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BusinessLayer.Models;
using DataAccessLayer.Models;

namespace BusinessLayer.Interfaces
{
    public interface IPostManager
    {
        IList<PostModel> Get();
        IList<PostModel> Get(Expression<Func<Post, bool>> expression);
        PostModel Get(int id);
        void Add(PostModel post);
        void RemoveById(int postId);
        void Update(PostModel post);
    }
}
