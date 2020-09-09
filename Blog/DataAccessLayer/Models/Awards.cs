using System.Collections.Generic;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Models
{
    public class Awards : IEntity<int>
    {
        public Awards()
        {
            Users = new List<ApplicationUser>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int PostReadCount { get; set; }
        public int PostWriteCount { get; set; }
        public int CommentWriteCount { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
    }
}
