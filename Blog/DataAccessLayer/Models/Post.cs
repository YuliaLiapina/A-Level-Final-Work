using System;
using System.Collections.Generic;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Models
{
    public class Post : IEntity<int>
    {
        public Post()
        {
            Comments = new List<Comment>();
        }
        public int Id { get; set; }
        public DateTime PublishDate { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Discription { get; set; }
        public string Body { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsShowComment { get; set; }
        public int UsersReadCount { get; set; }

        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
