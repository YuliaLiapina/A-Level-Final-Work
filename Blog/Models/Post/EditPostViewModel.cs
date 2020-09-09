using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Blog.Models
{
    public class EditPostViewModel
    {
        public int Id { get; set; }
        public string ReturnUrl { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [AllowHtml]
        [Required]
        [StringLength(10000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 200)]
        [Display(Name = "Article")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [Display(Name = "Image Url")]
        public string Image { get; set; }

        [Required]
        [StringLength(2000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 100)]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Discription { get; set; }

        [Display(Name = "Show comments")]
        public bool IsShowComment { get; set; }

        [Display(Name = "Is Blocked")]
        public bool IsBlocked { get; set; }
    }
}
