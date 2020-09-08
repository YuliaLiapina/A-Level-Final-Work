using DataAccessLayer;

namespace BusinessLayer.Models
{
    public class UserPostModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int PostId { get; set; }
        public PostModel Post { get; set; }
    }
}
