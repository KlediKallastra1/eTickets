using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class CinemaViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string? CinemaName { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Logo")]
        public string? LogoPath { get; set; }
    }
}
