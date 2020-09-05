using System;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Models
{
    public class Comment : IEntity<int>
    {
        public Comment()
        {

        }
        public int Id { get; set; }
        public DateTime PublishDate { get; set; }
        public string Body { get; set; }
        public bool IsBlocked { get; set; }

        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
