using eTickets.Data;
using eTickets.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [Display(Name = "Movie Name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        [Display(Name = "Movie Description")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Start Date is Required")]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "End Date is Required")]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Price is Required")]
        [Display(Name = "Price in $")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Movie Poster is Required")]
        [Display(Name = "Movie Poster URL")]
        public string? ImagePath { get; set; }

        [Required(ErrorMessage = "Movie Category is Required")]
        [Display(Name = "Select a Category")]
        public MovieCategory MovieCategory { get; set; }
        public List<Actor_Movie>? Actors_Movies { get; set; }

        [Required(ErrorMessage = "Select at least an actor")]
        [Display(Name = "Select actor(s)")]
        public List<int>? ActorsIds { get; set; }

        [Required(ErrorMessage = "Cinema is Required")]
        [Display(Name = "Select a Cinema")]
        public int CinemaId { get; set; }

        [ForeignKey("CinemaId")]
        public Cinema? Cinema { get; set; }

        [Required(ErrorMessage = "Producer is Required")]
        [Display(Name = "Select a Producer")]
        public int ProducerId { get; set; }

        [ForeignKey("ProducerId")]
        public Producer? Producer { get; set; }
    }
}
