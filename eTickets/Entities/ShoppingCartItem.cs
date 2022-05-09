using System.ComponentModel.DataAnnotations;

namespace eTickets.Entities
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }

        public Movie Movie { get; set; }

        public int MovieAmount { get; set; }

        public string ShoppingCartId { get; set; }
    }
}
