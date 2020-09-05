using System.Collections.Generic;
using BusinessLayer.Models;

namespace BusinessLayer.Interfaces
{
    public interface IPostManager
    {
        IList<PostModel> GetAll();
        PostModel GetById(int id);
        void Add(PostModel post);
    }
}
