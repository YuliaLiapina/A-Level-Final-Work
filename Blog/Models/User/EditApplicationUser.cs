using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class EditApplicationUser
    {
        public string Id { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Blocked User")]
        public bool IsUserBlocked { get; set; }
        public string ReturnUrl { get; set; }
    }
}
