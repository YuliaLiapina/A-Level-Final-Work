using System.Collections.Generic;
using DataAccessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface ICommentRepo
    {
        IList<Comment> GetAll();
    }
}
