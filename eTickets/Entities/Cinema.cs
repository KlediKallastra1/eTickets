using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Entities
{
    public class Cinema : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string? CinemaName { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Logo")]
        public string? LogoPath { get; set; }

        //Relationships
        public List<Movie>? Movies { get; set; }
    }
}
