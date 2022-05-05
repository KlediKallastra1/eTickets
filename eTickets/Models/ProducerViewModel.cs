using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class ProducerViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "The full name of the Producer is required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The full name must be between 3 and 50 characters!")]
        public string? ProducerName { get; set; }

        [Display(Name = "Biography")]
        public string? Biography { get; set; }

        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "A profile picture for the Producer is required!")]
        public string? ProfilePicturePath { get; set; }
    }
}
