using eTickets.Data.Cart;

namespace eTickets.Models
{
    public class ShoppingCartViewModel
    {
        public ShoppingCart Cart { get; set; }
        public double CartTotal { get; set; }
    }
}
