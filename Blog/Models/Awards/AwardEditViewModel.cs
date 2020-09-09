using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class AwardEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
