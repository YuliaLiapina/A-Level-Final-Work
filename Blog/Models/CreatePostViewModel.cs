using System.Web.Mvc;

namespace Blog.Models
{
    public class CreatePostViewModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        public string Discription { get; set; }
        public bool IsShowComment { get; set; }
    }
}
