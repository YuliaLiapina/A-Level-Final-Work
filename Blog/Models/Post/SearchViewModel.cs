using System.Collections.Generic;

namespace Blog.Models
{
    public class SearchViewModel
    {
        public IList<PostViewModel> Posts { get; set; }
        public string SearchString { get; set; }
    }
}
