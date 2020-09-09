using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class ApplicationUserModel
    {
        public ApplicationUserModel()
        {
            Awards = new List<AwardsModel>();
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool IsUserBlocked { get; set; }
        public int PostsReadCount { get; set; }
        public int PostsWriteCount { get; set; }
        public int CommentsWriteCount { get; set; }

        public ICollection<AwardsModel> Awards { get; set; }
    }
}
