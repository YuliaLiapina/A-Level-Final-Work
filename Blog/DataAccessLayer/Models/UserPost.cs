using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Models
{
    public class UserPost : IEntity<int>
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
