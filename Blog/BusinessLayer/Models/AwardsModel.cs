using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class AwardsModel
    {
        public AwardsModel()
        {
            Users = new List<ApplicationUserModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int PostReadCount { get; set; }
        public int PostWriteCount { get; set; }
        public int CommentWriteCount { get; set; }

        public ICollection<ApplicationUserModel> Users { get; set; }
    }
}
