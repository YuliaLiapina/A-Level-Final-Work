using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class AddCommentViewModel
    {
        public int PostId { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [Display(Name = "Comment")]
        public string Body { get; set; }
    }
}
