namespace Blog.Models
{
    public class PostDetailsViewModel
    {
        public PostViewModel Post { get; set; }
        public bool IsUserBlocked { get; set; }
        public int UserPostsReadCount { get; set; }
        public int UserCommentsWriteCount { get; set; }
    }
}
