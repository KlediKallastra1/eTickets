using eTickets.Data.Cart;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMoviesService _service;
        private readonly ShoppingCart _cart;
        private readonly IOrdersService _ordersService;

        public OrdersController(IMoviesService service, ShoppingCart cart, IOrdersService ordersService)
        {
            _service = service;
            _cart = cart;
            _ordersService = ordersService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = "";
            var orders = await _ordersService.GetOrdersByUserIdAsync(userId);
            return View(orders);
        }

        public IActionResult ShoppingCart()
        {
            var data = _cart.GetShoppingCartItems();
            _cart.ShoppingCartItems = data;
            var response = new ShoppingCartViewModel
            {
                Cart = _cart,
                CartTotal = _cart.GetShoppingCartTotal()
            };
            return View(response);
        }

        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var movie = await _service.GetMovieByIdAsync(id);
            if(movie != null) _cart.AddItemToShoppingCart(movie);
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemToShoppingCart(int id)
        {
            var movie = await _service.GetMovieByIdAsync(id);
            if (movie != null) _cart.RemoveItemFromShoppingCart(movie);
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _cart.GetShoppingCartItems();
            string userId = "";
            string userEmailAddress = "";

            await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
            await _cart.ClearCartAsync();

            return View("OrderCompleted");
        }
    }
}
