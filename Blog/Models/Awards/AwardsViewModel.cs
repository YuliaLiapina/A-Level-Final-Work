using System.Collections.Generic;

namespace Blog.Models
{
    public class AwardsViewModel
    {
        public AwardsViewModel()
        {
            Users = new List<ApplicationUserViewModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int PostReadCount { get; set; }
        public int PostWriteCount { get; set; }
        public int CommentWriteCount { get; set; }

        public ICollection<ApplicationUserViewModel> Users { get; set; }
    }
}
