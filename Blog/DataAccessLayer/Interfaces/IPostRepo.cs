using System.Collections.Generic;
using DataAccessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IPostRepo
    {
        IList<Post> GetAll();
        
        Post GetById(int id);
    }
}
