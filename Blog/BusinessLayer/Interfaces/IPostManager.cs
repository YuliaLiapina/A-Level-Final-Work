using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BusinessLayer.Models;
using DataAccessLayer.Models;

namespace BusinessLayer.Interfaces
{
    public interface IPostManager
    {
        IList<PostModel> GetAll();
        IList<PostModel> GetAll(Expression<Func<Post, bool>> expression);
        PostModel GetById(int id);
        void Add(PostModel post);
        IList<PostModel> GetByAuthorId(string authorId);
        void RemoveById(int postId);
        void Update(PostModel post);
    }
}
