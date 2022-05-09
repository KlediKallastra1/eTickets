using eTickets.Entities;

namespace eTickets.Data.Services
{
    public interface IOrdersService
    {
        public Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAdrress);
        public Task<List<Order>> GetOrdersByUserIdAsync(string userId);
    }
}
