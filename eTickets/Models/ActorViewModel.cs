using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class ActorViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Actors Name is required")]
        public string? ActorName { get; set; }

        [Display(Name = "Biography")]
        public string? Biography { get; set; }

        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "A profile picture is required")]
        public string? ProfilePicturePath { get; set; }
    }
}
