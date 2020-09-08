using System.Collections.Generic;
using DataAccessLayer;

namespace Blog.Models
{
    public class AllPostsViewModel
    {
        public IList<PostViewModel> Posts { get; set; }
        public int MaxPage { get; set; }
        public int Page { get; set; }
        public string AuthorId { get; set; }
        public bool IsUserBlocked { get; set; }
        public IList<ApplicationUser> Authors { get; set; }
    }
}
