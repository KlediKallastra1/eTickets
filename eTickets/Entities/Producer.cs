using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Entities
{
    public class Producer : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string? ProducerName { get; set; }

        [Display(Name = "Biography")]
        public string? Biography { get; set; }

        [Display(Name = "Profile Picture")]
        public string? ProfilePicturePath { get; set; }
        //Relationships
        public List<Movie>? Movies { get; set; }
    }
}
