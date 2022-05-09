using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email address is required")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
