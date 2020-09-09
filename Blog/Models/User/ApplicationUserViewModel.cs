using System.Collections.Generic;

namespace Blog.Models
{
    public class ApplicationUserViewModel
    {
        public ApplicationUserViewModel()
        {
            Awards = new List<AwardsViewModel>();
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool IsUserBlocked { get; set; }
        public int PostsReadCount { get; set; }
        public int PostsWriteCount { get; set; }
        public int CommentsWriteCount { get; set; }

        public ICollection<AwardsViewModel> Awards { get; set; }
    }
}
