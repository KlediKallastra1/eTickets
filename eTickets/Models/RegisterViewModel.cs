using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email address is required")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "A Password for confirmation is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "PAsswords do not match!")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "A Date is required")]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "A profile picture is required")]
        [Display(Name = "Profile Picture")]
        public string ProfilePicturePath { get; set; }
    }
}
